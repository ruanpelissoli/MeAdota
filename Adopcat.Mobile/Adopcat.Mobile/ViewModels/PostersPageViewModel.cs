using Adopcat.Mobile.Models;
using Adopcat.Mobile.Views;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
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
        public DelegateCommand<PosterOutput> ShowCarouselArrowsCommand;

        private bool _isNotFirstItem;
        public bool IsNotFirstItem
        {
            get { return _isNotFirstItem; }
            set { SetProperty(ref _isNotFirstItem, value); }
        }

        private bool _isNotLastItem;
        public bool IsNotLastItem
        {
            get { return _isNotLastItem; }
            set { SetProperty(ref _isNotLastItem, value); }
        }

        public PostersPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService, dialogService)
        {
            Title = "Anúncios";

            FilterCommand = new DelegateCommand(FilterCommandExecute);
            PosterSelectedCommand = new DelegateCommand<int?>(PosterSelectedCommandExecute);
            ShowCarouselArrowsCommand = new DelegateCommand<PosterOutput>(ShowCarouselArrowsCommandExecute);
        }

        private void ShowCarouselArrowsCommandExecute(PosterOutput poster)
        {
            IsNotFirstItem = true;
            IsNotLastItem = true;

            if (Posters.First() == poster)
                IsNotFirstItem = false;

            if (Posters.Last() == poster)
                IsNotLastItem = false;
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

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            try
            {
                Posters = new ObservableCollection<PosterOutput>(parameters.GetValue<List<PosterOutput>>("posters"));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
        }
    }
}
