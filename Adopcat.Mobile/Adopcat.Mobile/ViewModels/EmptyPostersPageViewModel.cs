using Adopcat.Mobile.Views;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System.Diagnostics;

namespace Adopcat.Mobile.ViewModels
{
    public class EmptyPostersPageViewModel : BaseViewModel
    {
        public DelegateCommand RefreshCommand { get; set; }

        public EmptyPostersPageViewModel(
            INavigationService navigationService,
            IPageDialogService dialogService) : base(navigationService, dialogService)
        {
            Title = "Anúncios";
            RefreshCommand = new DelegateCommand(RefreshCommandExecute);
        }

        private async void RefreshCommandExecute()
        {
            ShowLoading = true;
            try
            {
                await _navigationService.NavigateAsync($"app:///{nameof(MenuPage)}/NavigationPage/{nameof(PostersPage)}");
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                ShowLoading = false;
            }
        }
    }
}
