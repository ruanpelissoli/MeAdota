using MeAdota.Mobile.Util;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System.Threading.Tasks;

namespace MeAdota.Mobile.ViewModels
{
    public class BaseViewModel : BindableBase, INavigationAware, IPageDialogService
    {
        protected INavigationService _navigationService;
        protected IPageDialogService _dialogService;

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private bool _showLoading;
        public bool ShowLoading
        {
            get { return _showLoading; }
            set { SetProperty(ref _showLoading, value); }
        }

        public BaseViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;

            Title = string.Empty;
        }

        protected async Task<EPictureOptions> DisplayPictureAlert()
        {
            var action = await _dialogService.DisplayActionSheetAsync("Foto", "Fechar", null, "Tirar foto", "Álbum");

            if (action == null) return EPictureOptions.Invalid;

            if (action.ToLower().Equals("foto")) return EPictureOptions.Take;

            return EPictureOptions.Pick;
        }

        public virtual void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }

        public virtual void OnNavigatedTo(NavigationParameters parameters)
        {
            
        }

        public virtual void OnNavigatingTo(NavigationParameters parameters)
        {
            
        }

        public virtual async Task<bool> DisplayAlertAsync(string title, string message, string acceptButton, string cancelButton)
        {
            return await _dialogService.DisplayAlertAsync(title, message, acceptButton, cancelButton);
        }

        public virtual async Task DisplayAlertAsync(string title, string message, string cancelButton)
        {
            await _dialogService.DisplayAlertAsync(title, message, cancelButton);
        }

        public virtual async Task<string> DisplayActionSheetAsync(string title, string cancelButton, string destroyButton, params string[] otherButtons)
        {
            return await _dialogService.DisplayActionSheetAsync(title, cancelButton, destroyButton, otherButtons);
        }

        public virtual async Task DisplayActionSheetAsync(string title, params IActionSheetButton[] buttons)
        {
            await _dialogService.DisplayActionSheetAsync(title, buttons);
        }
    }
}
