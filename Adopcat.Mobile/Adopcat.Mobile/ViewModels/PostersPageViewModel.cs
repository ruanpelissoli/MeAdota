using Adopcat.Mobile.Helpers;
using Adopcat.Mobile.Models;
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

        public PostersPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService, dialogService)
        {
            Title = "Anúncios";
            FilterCommand = new DelegateCommand(FilterCommandExecute);
        }

        private async void FilterCommandExecute()
        {
            await _navigationService.NavigateAsync(nameof(FilterPage), null, true);
        }

        public async override void OnNavigatedTo(NavigationParameters parameters)
        {
            ShowLoading = true;
            base.OnNavigatedTo(parameters);

            try
            {
                if (parameters.Any(a => a.Key.Equals("filter")))
                {

                }

                Posters = new ObservableCollection<PosterOutput>(
                    await App.ApiService.GetPosters("bearer " + Settings.AuthToken));

                foreach (var poster in Posters)
                {
                    poster.MainPictureUrl = poster.PetPictures.FirstOrDefault()?.Url;
                }
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
    }
}
