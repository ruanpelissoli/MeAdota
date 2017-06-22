using System;
using Prism.Commands;
using Prism.Navigation;
using Adopcat.Mobile.Models;
using System.Diagnostics;
using Prism.Services;
using Adopcat.Mobile.Views;
using Plugin.Media.Abstractions;
using Adopcat.Mobile.Services;
using System.IO;

namespace Adopcat.Mobile.ViewModels
{
    public class RegisterPageViewModel : BaseViewModel
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                SetProperty(ref _name, value);
                RegisterCommand.RaiseCanExecuteChanged();
            }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                SetProperty(ref _email, value);
                RegisterCommand.RaiseCanExecuteChanged();
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                SetProperty(ref _password, value);
                RegisterCommand.RaiseCanExecuteChanged();
            }
        }

        private string _passwordConfirm;
        public string PasswordConfirm
        {
            get { return _passwordConfirm; }
            set
            {
                SetProperty(ref _passwordConfirm, value);
                RegisterCommand.RaiseCanExecuteChanged();
            }
        }

        private string _phone;
        public string Phone
        {
            get { return _phone; }
            set
            {
                SetProperty(ref _phone, value);
                RegisterCommand.RaiseCanExecuteChanged();
            }
        }

        private byte[] _image;
        public byte[] Image
        {
            get { return _image; }
            set
            {
                SetProperty(ref _image, value);
                RegisterCommand.RaiseCanExecuteChanged();
            }
        }

        public DelegateCommand RegisterCommand { get; set; }
        public DelegateCommand PickPhotoCommand { get; set; }

        public RegisterPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService, dialogService)
        {
            Title = "Registre-se";

            RegisterCommand = new DelegateCommand(RegisterCommandExecute, RegisterCommandCanExecute);
            PickPhotoCommand = new DelegateCommand(PickPhotoCommandExecute);
        }

        private bool RegisterCommandCanExecute()
        {
            return !string.IsNullOrEmpty(Name) &&
                   !string.IsNullOrEmpty(Email) &&
                   !string.IsNullOrEmpty(Password) &&
                   !string.IsNullOrEmpty(PasswordConfirm) &&
                   Password == PasswordConfirm &&
                   !string.IsNullOrEmpty(Phone) &&
                   Image != null;
        }

        private async void RegisterCommandExecute()
        {
            try
            {
                ShowLoading = true;

                var user = new User
                {
                    Email = Email,
                    Name = Name,
                    Password = Password,
                    Phone = Phone,
                    Picture = Image
                };

                await App.ApiService.CreateUser(user);

                await _dialogService.DisplayAlertAsync("Sucesso", "Usuário criado com sucesso.", "Ok");

                ShowLoading = false;
                await _navigationService.NavigateAsync($"app:///NavigationPage/{nameof(LoginPage)}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
        }

        private async void PickPhotoCommandExecute()
        {
            try
            {
                var action = await _dialogService.DisplayActionSheetAsync("Foto", "Cancel", null, "Tirar foto", "Álbum");

                MediaFile file = null;
                var pictureService = Xamarin.Forms.DependencyService.Get<PictureService>();

                if (action.ToLower().Equals("tirar foto"))
                    file = await pictureService.TakePhotoAsync();
                else if (action.ToLower().Equals("álbum"))
                    file = await pictureService.PickPhotoAsync();
                else return;

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
            catch (Exception ex)
            {
                await _dialogService.DisplayAlertAsync("Erro", ex.Message, "Fechar");
            }            
        }
    }
}
