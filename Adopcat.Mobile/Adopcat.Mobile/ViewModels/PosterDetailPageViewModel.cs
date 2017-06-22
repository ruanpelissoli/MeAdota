using Adopcat.Mobile.Helpers;
using Adopcat.Mobile.Models;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System.Diagnostics;
using System.Linq;
using Adopcat.Mobile.Interfaces;
using System;

namespace Adopcat.Mobile.ViewModels
{
    public class PosterDetailPageViewModel : BaseViewModel
    {
        private PosterOutput _poster;

        public DelegateCommand CallCommand { get; set; }

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

        public PosterOutput Poster
        {
            get { return _poster; }
            set { SetProperty(ref _poster, value); }
        }

        public PosterDetailPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
            : base(navigationService, dialogService)
        {
            CallCommand = new DelegateCommand(CallCommandExecute);
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
