using Adopcat.Mobile.Helpers;
using Adopcat.Mobile.Models;
using Prism.Navigation;
using Prism.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace Adopcat.Mobile.ViewModels
{
    public class PosterDetailPageViewModel : BaseViewModel
    {
        private PosterOutput _poster;
        public PosterOutput Poster
        {
            get { return _poster; }
            set { SetProperty(ref _poster, value); }
        }

        public PosterDetailPageViewModel(INavigationService navigationService, IPageDialogService dialogService) 
            : base(navigationService, dialogService)
        {
           
        }

        public async override void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            try
            {
                if (parameters.Any(a => a.Key.Equals("posterId")))
                {
                    Poster = await App.ApiService.GetPoster(parameters.GetValue<int>("posterId"), "bearer " + Settings.AuthToken);                    
                }
            }
            catch (Refit.ApiException ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
        }
    }
}
