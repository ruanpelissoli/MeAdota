using Adopcat.Mobile.Views;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace Adopcat.Mobile.ViewModels
{
    public class ConfigurationPageViewModel : BaseViewModel
    {
        public DelegateCommand LogoutCommand { get; set; }

        public ConfigurationPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService, dialogService)
        {
            LogoutCommand = new DelegateCommand(LogoutCommandExecute);
        }

        private async void LogoutCommandExecute()
        {
            await App.MobileService.LogoutAsync();
            await _navigationService.NavigateAsync(nameof(LoginPage));
        }
    }
}
