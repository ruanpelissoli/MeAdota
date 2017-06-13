using Adopcat.Mobile.Views;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace Adopcat.Mobile.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {
        public DelegateCommand LoginCommand { get; set; }
        public DelegateCommand RegisterCommand { get; set; }

        public LoginPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService, dialogService)
        {
            Title = "Login";

            LoginCommand = new DelegateCommand(LoginCommandExecute);
            RegisterCommand = new DelegateCommand(RegisterCommandExecute);
        }

        private async void RegisterCommandExecute()
        {
            await _navigationService.NavigateAsync(nameof(RegisterPage));
        }

        private async void LoginCommandExecute()
        {
            await _navigationService.NavigateAsync(nameof(LoginDataPage));
        }
    }
}
