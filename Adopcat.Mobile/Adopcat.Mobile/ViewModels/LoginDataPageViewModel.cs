using System;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System.Diagnostics;
using Adopcat.Mobile.Models;
using Adopcat.Mobile.Helpers;
using Adopcat.Mobile.Views;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace Adopcat.Mobile.ViewModels
{
    public class LoginDataPageViewModel : BaseViewModel
    {
        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                SetProperty(ref _email, value);
                LoginCommand.RaiseCanExecuteChanged();
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                SetProperty(ref _password, value);
                LoginCommand.RaiseCanExecuteChanged();
            }
        }

        public DelegateCommand LoginCommand { get; set; }
        public DelegateCommand FacebookLoginCommand { get; set; }

        public LoginDataPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService, dialogService)
        {
            Title = "Login";

            LoginCommand = new DelegateCommand(LoginCommandExecute, LoginCanExecuteCommand);
            FacebookLoginCommand = new DelegateCommand(FacebookLoginCommandExecute);
        }

        private async void LoginCommandExecute()
        {
            try
            {
                ShowLoading = true;

                var model = new Login
                {
                    Email = Email,
                    Password = Password
                };

                var loginResponse = await App.ApiService.Login(model);
                loginResponse.AuthToken = loginResponse.AuthToken.Replace("\"", "");
                Settings.AuthToken = loginResponse.AuthToken;
                Settings.UserId = loginResponse.UserId.ToString();

                await _navigationService.NavigateAsync($"app:///{nameof(MenuPage)}/NavigationPage/{nameof(PostersPage)}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                ShowLoading = false;
            }
        }

        private bool LoginCanExecuteCommand()
        {
            return !string.IsNullOrEmpty(Email) &&
                   !string.IsNullOrEmpty(Password);
        }

        private async void FacebookLoginCommandExecute()
        {
            if (!(await LoginAsync()))
                return;

            await _navigationService.NavigateAsync($"app:///{nameof(MenuPage)}/NavigationPage/{nameof(PostersPage)}");
        }

        private async Task<bool> LoginAsync()
        {
            if (Settings.IsLoggedIn)
                return await Task.FromResult(true);

            return await App.MobileService.LoginAsync();
        }
    }
}
