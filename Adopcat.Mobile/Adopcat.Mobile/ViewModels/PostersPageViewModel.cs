using Adopcat.Mobile.Helpers;
using Adopcat.Mobile.Models;
using Adopcat.Mobile.Services;
using Adopcat.Mobile.Views;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace Adopcat.Mobile.ViewModels
{
    public class PostersPageViewModel : BaseViewModel
    {
        private ObservableCollection<PosterOutput> _posters;
        public ObservableCollection<PosterOutput> Posters
        {
            get { return _posters; }
            set { SetProperty(ref _posters, value); }
        }

        public DelegateCommand FilterCommand { get; set; }
        public DelegateCommand<int?> PosterSelectedCommand { get; set; }

        public PostersPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService, dialogService)
        {
            Title = "Anúncios";

            FilterCommand = new DelegateCommand(FilterCommandExecute);
            PosterSelectedCommand = new DelegateCommand<int?>(PosterSelectedCommandExecute);
        }

        private async void PosterSelectedCommandExecute(int? posterId)
        {
            if (posterId != null)
            {
                var parameters = new NavigationParameters
                {
                    { "posterId", posterId.Value }
                };
                await _navigationService.NavigateAsync(nameof(PosterDetailPage), parameters);
            }
        }

        private async void FilterCommandExecute()
        {
            await _navigationService.NavigateAsync($"NavigationPage/{nameof(FilterPage)}", null, true);
        }

        public async override void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            try
            {
                Filter filter = null;
                if (parameters.Any(a => a.Key.Equals("filter")))
                {
                    filter = parameters.GetValue<Filter>("filter");
                }

                if (filter != null)
                {
                    Posters = new ObservableCollection<PosterOutput>(
                                    await App.ApiService.GetFilteredPosters(int.Parse(Settings.UserId), filter, "bearer " + Settings.AuthToken));
                }
                else
                {
                    Posters = new ObservableCollection<PosterOutput>(
                                    await App.ApiService.GetPosters(int.Parse(Settings.UserId), "bearer " + Settings.AuthToken));
                }

                foreach (var poster in Posters)
                {
                    poster.MainPictureUrl = poster.PetPictures.FirstOrDefault()?.Url;
                }
            }
            catch (Refit.ApiException ex)
            {
                if (ex.ReasonPhrase.Equals("Unauthorized"))
                    await App.MobileService.LogoutAsync();
                Debug.WriteLine(ex.StackTrace);
            }
        }
    }
}
