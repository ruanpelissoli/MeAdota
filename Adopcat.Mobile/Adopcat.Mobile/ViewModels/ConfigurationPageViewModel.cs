using System;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Adopcat.Mobile.Views;
using Adopcat.Mobile.Services;
using System.IO;
using System.Diagnostics;
using Adopcat.Mobile.Models;
using Adopcat.Mobile.Helpers;

namespace Adopcat.Mobile.ViewModels
{
    public class ConfigurationPageViewModel : BaseViewModel
    {
        private User _user;
        public User User
        {
            get { return _user; }
            set { SetProperty(ref _user, value); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                SetProperty(ref _password, value);
                SaveInfoCommand.RaiseCanExecuteChanged();
            }
        }

        private string _newPassword;
        public string NewPassword
        {
            get { return _newPassword; }
            set
            {
                SetProperty(ref _newPassword, value);
                SaveInfoCommand.RaiseCanExecuteChanged();
            }
        }

        private byte[] _image;
        public byte[] Image
        {
            get { return _image; }
            set
            {
                SetProperty(ref _image, value);
            }
        }

        private bool _isNotFacebookUser;
        public bool IsNotFacebookUser
        {
            get { return _isNotFacebookUser; }
            set { SetProperty(ref _isNotFacebookUser, value); }
        }

        public DelegateCommand SaveInfoCommand { get; set; }
        public DelegateCommand LogoutCommand { get; set; }
        public DelegateCommand PickPictureCommand { get; set; }

        public ConfigurationPageViewModel(
            INavigationService navigationService,
            IPageDialogService dialogService) : base(navigationService, dialogService)
        {
            Title = "Configurações";

            SaveInfoCommand = new DelegateCommand(SaveInfoCommandExecute, SaveInfoCommandCanExecute);
            LogoutCommand = new DelegateCommand(LogoutCommandExecute);
            PickPictureCommand = new DelegateCommand(PickPictureCommandExecute);
        }

        private async void PickPictureCommandExecute()
        {
            var action = await DisplayPictureAlert();

            var pictureService = Xamarin.Forms.DependencyService.Get<PictureService>();
            var file = await pictureService.GetPicture(action);

            if (file != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    file.GetStream().CopyTo(memoryStream);
                    file.Dispose();
                    Image = memoryStream.ToArray();
                }
            }
        }

        private async void SaveInfoCommandExecute()
        {
            ShowLoading = true;
            try
            {
                if(!IsNotFacebookUser && !string.IsNullOrEmpty(Password))
                {
                    User.Picture = Image;
                    User.Password = Password;
                }
                await App.ApiService.UpdateUser(User.Email, User, "bearer " + Settings.AuthToken);
                await _dialogService.DisplayAlertAsync("Sucesso!", "Informações alteradas com sucesso.", "Ok");

                Password = string.Empty;
                NewPassword = string.Empty;

                SaveInfoCommand.RaiseCanExecuteChanged();
            }
            catch (Exception ex)
            {
                await ExceptionHandler.Handle(ex);
            }
            finally
            {
                ShowLoading = false;
            }
        }

        private bool SaveInfoCommandCanExecute()
        {
            if (!IsNotFacebookUser) return true;

            if (!string.IsNullOrEmpty(Password) || !string.IsNullOrEmpty(NewPassword))
            {
                if (!Password.Equals(NewPassword)) return false;
            }
            return true;
        }

        private async void LogoutCommandExecute()
        {
            if (await _dialogService.DisplayAlertAsync("Logout", "Tem certeza que deseja sair?", "Sim", "Não"))
            {
                await App.MobileService.LogoutAsync();
                await _navigationService.NavigateAsync($"app:///NavigationPage/{nameof(LoginPage)}");
            }
        }

        public async override void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            try
            {
                User = await App.ApiService.GetUser(int.Parse(Settings.UserId));

                IsNotFacebookUser = string.IsNullOrEmpty(User.FacebookId);

                if (IsNotFacebookUser)
                    Image = await App.ApiService.Download(User.PictureUrl);
            }
            catch (Exception ex)
            {
                await ExceptionHandler.Handle(ex);
            }
        }
    }
}
