using Adopcat.Mobile.Helpers;
using Adopcat.Mobile.Models;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Adopcat.Mobile.ViewModels
{
    public class MyPostersPageViewModel : BaseViewModel
    {
        private ObservableCollection<PosterOutput> _myPosters;
        public ObservableCollection<PosterOutput> MyPosters
        {
            get { return _myPosters; }
            set { SetProperty(ref _myPosters, value); }
        }

        public DelegateCommand<int> SelectedPosterCommand { get; set; }

        public MyPostersPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService, dialogService)
        {
            Title = "Meus Anúncios";

            SelectedPosterCommand = new DelegateCommand<int>(SelectedPosterCommandExecute);
        }

        public async override void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            try
            {
                MyPosters = new ObservableCollection<PosterOutput>(
                                    await App.ApiService.GetMyPosters(int.Parse(Settings.UserId), Settings.AuthToken));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
        }

        private void SelectedPosterCommandExecute(int posterId)
        {
            
        }
    }
}
