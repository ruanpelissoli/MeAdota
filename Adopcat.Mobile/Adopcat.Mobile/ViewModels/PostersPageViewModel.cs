using Adopcat.Mobile.Helpers;
using Adopcat.Mobile.Models;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Adopcat.Mobile.ViewModels
{
    public class PostersPageViewModel : BaseViewModel
    {
        private ObservableCollection<Poster> _posters;
        public ObservableCollection<Poster> Posters
        {
            get { return _posters; }
            set { SetProperty(ref _posters, value); }
        }

        public PostersPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService, dialogService)
        {
            Title = "Anúncios";
        }

        public async override void OnNavigatedTo(NavigationParameters parameters)
        {
            ShowLoading = true;
            base.OnNavigatedTo(parameters);

            try
            {
                Posters = new ObservableCollection<Poster>(
                    await App.ApiService.GetPosters("bearer " + Settings.AuthToken));                
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
