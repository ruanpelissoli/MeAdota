using Adopcat.Mobile.Helpers;
using Adopcat.Mobile.Models;
using Prism.Services;
using System;
using System.Threading.Tasks;

namespace Adopcat.Mobile.Util
{
    public static class ExceptionHandler
    {
        private static SystemLog CreateLog(string errorMessage)
        {
            return new SystemLog
            {
                Text = errorMessage,
                Platform = EPlatform.Mobile,
                LogType = ELogType.Error,
                LogDate = DateTime.Now
            };
        }

        public async static Task HandleException(Exception ex)
        {
            try
            {
                var log = CreateLog(ex is Refit.ApiException ? (ex as Refit.ApiException).ReasonPhrase : ex.Message);
                await App.ApiService.CreateLog(log, "bearer " + Settings.AuthToken);
            }
            catch(Exception) { }
        }

        public async static Task HandleException(Exception ex, IPageDialogService dialogService)
        {
            try
            {
                var log = CreateLog(ex is Refit.ApiException ? (ex as Refit.ApiException).ReasonPhrase : ex.Message);
                await App.ApiService.CreateLog(log, "bearer " + Settings.AuthToken);
            }
            catch (Exception) { }

            await dialogService.DisplayAlertAsync("Erro!", ex.Message, "Ok");
        }
    }
}
