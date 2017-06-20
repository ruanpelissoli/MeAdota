using Adopcat.Mobile.Helpers;
using Adopcat.Mobile.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Adopcat.Mobile.ViewModels
{
    public class EditMyPosterPageViewModel : BaseViewModel
    {
        private PosterOutput _poster;
        public PosterOutput Poster
        {
            get { return _poster; }
            set { SetProperty(ref _poster, value); }
        }

        public EditMyPosterPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService, dialogService)
        {
            Title = "Editar anúncio";
        }

        public async override void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.Any(a => a.Key.Equals("posterId")))
            {
                Poster = await App.ApiService.GetPoster(parameters.GetValue<int>("posterId"), "bearer " + Settings.AuthToken);
            }
        }
    }
}
