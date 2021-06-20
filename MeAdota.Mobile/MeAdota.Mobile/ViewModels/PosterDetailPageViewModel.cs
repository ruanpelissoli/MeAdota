using MeAdota.Mobile.Helpers;
using MeAdota.Mobile.Models;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System.Diagnostics;
using System.Linq;
using MeAdota.Mobile.Interfaces;
using System;
using MeAdota.Mobile.Views;

namespace MeAdota.Mobile.ViewModels
{
    public class PosterDetailPageViewModel : BaseViewModel
    {
        public DelegateCommand CallCommand { get; set; }
        public DelegateCommand ReportPosterCommand { get; set; }

        private int _position;
        public int Position
        {
            get { return _position; }
            set { SetProperty(ref _position, value); }
        }

        private string _imgCastrated;
        public string ImgCastrated
        {
            get { return _imgCastrated; }
            set { SetProperty(ref _imgCastrated, value); }
        }

        private string _imgDewormed;
        public string ImgDewormed
        {
            get { return _imgDewormed; }
            set { SetProperty(ref _imgDewormed, value); }
        }

        private PosterOutput _poster;
        public PosterOutput Poster
        {
            get { return _poster; }
            set { SetProperty(ref _poster, value); }
        }

        public PosterDetailPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
            : base(navigationService, dialogService)
        {
            CallCommand = new DelegateCommand(CallCommandExecute);
            ReportPosterCommand = new DelegateCommand(ReportPosterCommandExecute);
        }

        private async void ReportPosterCommandExecute()
        {
            var parameters = new NavigationParameters
            {
                { "posterId", Poster.Id }
            };
            await _navigationService.NavigateAsync($"NavigationPage/{nameof(ReportPosterPage)}", parameters, true);
        }

        private async void CallCommandExecute()
        {
            try
            {
                if (!string.IsNullOrEmpty(Poster.User.Phone))
                    Xamarin.Forms.DependencyService.Get<IPhoneCall>().CallNumber(Poster.User.Phone);
            }
            catch (Exception ex)
            {
                await _dialogService.DisplayAlertAsync("Erro", ex.Message, "Fechar");
            }
        }

        public async override void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            try
            {
                if (parameters.Any(a => a.Key.Equals("posterId")))
                {
                    Poster = await App.ApiService.GetPoster(parameters.GetValue<int>("posterId"), "bearer " + Settings.AuthToken);

                    ImgCastrated = Poster.Castrated ? "icon_checked.png" : "icon_not_checked.png";
                    ImgDewormed = Poster.Dewormed ? "icon_checked.png" : "icon_not_checked.png";
                }
            }
            catch (Refit.ApiException ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
        }
    }
}
