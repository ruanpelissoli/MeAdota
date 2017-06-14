using Prism.Navigation;
using Prism.Services;

namespace Adopcat.Mobile.ViewModels
{
    public class FilterPageViewModel : BaseViewModel
    {
        public FilterPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService, dialogService)
        {

        }

        public override void OnNavigatedFrom(NavigationParameters parameters)
        {
            parameters.Add("filter", "teste");

            base.OnNavigatedFrom(parameters);
        }
    }
}
