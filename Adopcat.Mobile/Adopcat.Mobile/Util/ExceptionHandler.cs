using Adopcat.Mobile.Helpers;
using Adopcat.Mobile.Models;
using Adopcat.Mobile.Views;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Threading.Tasks;

namespace Adopcat.Mobile.Util
{
    public class ExceptionHandler
    {
        private INavigationService _navigationService;
        private IPageDialogService _dialogService;

        public ExceptionHandler(INavigationService navigationService, IPageDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
        }

        public ExceptionHandler()
        {
            
        }

        private SystemLog CreateLog(string errorMessage)
        {
            return new SystemLog
            {
                Text = errorMessage,
                Platform = EPlatform.Mobile,
                LogType = ELogType.Error,
                LogDate = DateTime.Now
            };
        }

        public async Task Handle(Exception ex, bool showAlert = false)
        {
            Refit.ApiException apiException = null;
            try
            {
                if (!string.IsNullOrEmpty(Settings.AuthToken))
                {
                    if (ex is Refit.ApiException)
                        apiException = (ex as Refit.ApiException);

                    var log = CreateLog(apiException != null ? apiException.ReasonPhrase : ex.Message);

                    await App.ApiService.CreateLog(log, "bearer " + Settings.AuthToken);
                }
            }
            catch (Exception) { }

            if (apiException != null && apiException.Equals("Unauthorized"))
            {
                await App.MobileService.LogoutAsync();
                await _navigationService.NavigateAsync($"app:///NavigationPage/{nameof(LoginPage)}");
                return;
            }

            if (showAlert)
                await _dialogService.DisplayAlertAsync("Erro!", "Ocorreu um erro inesperado, verifique sua conexão e tente novamente.", "Ok");
        }
    }
}
