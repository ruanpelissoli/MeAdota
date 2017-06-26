using Adopcat.Mobile.Helpers;
using Adopcat.Mobile.Interfaces;
using Adopcat.Mobile.Models;
using Adopcat.Mobile.Views;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Adopcat.Mobile.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
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
        public DelegateCommand RegisterCommand { get; set; }
        public DelegateCommand FacebookLoginCommand { get; set; }
        
        public LoginPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService, dialogService)
        {
            Title = "MeAdota! - Login";

            LoginCommand = new DelegateCommand(LoginCommandExecute, LoginCanExecuteCommand);
            RegisterCommand = new DelegateCommand(RegisterCommandExecute);
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

                await _navigationService.NavigateAsync($"app:///{nameof(MenuPage)}/NavigationPage/{nameof(LoadingPostersPage)}");
            }
            catch (Exception ex)
            {
                if(ex is Refit.ApiException)
                {
                    var refiEx = ex as Refit.ApiException;
                    if (refiEx.Content.Equals("Unauthorized")) {
                        await _dialogService.DisplayAlertAsync("Erro!", "Usuário ou senha inválidos.", "Ok");
                    }
                }
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
            ShowLoading = true;
            if (!(await LoginAsync()))
                return;

            await _navigationService.NavigateAsync($"app:///{nameof(MenuPage)}/NavigationPage/{nameof(LoadingPostersPage)}");

            ShowLoading = false;
        }

        private async Task<bool> LoginAsync()
        {
            if (Settings.IsLoggedIn)
                return await Task.FromResult(true);

            return await App.MobileService.LoginAsync();
        }

        private async void RegisterCommandExecute()
        {
            await _navigationService.NavigateAsync(nameof(RegisterPage));
        }
    }
}
