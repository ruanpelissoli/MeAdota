using Adopcat.Mobile.Helpers;
using Adopcat.Mobile.Interfaces;
using Adopcat.Mobile.Services;
using Adopcat.Mobile.Util;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(AzureService))]
namespace Adopcat.Mobile.Services
{
    public class AzureService
    {
        IAuthentication _auth;

        public MobileServiceClient Client { get; set; } = null;
        public static bool UseAuth { get; set; } = false;

        public void Initialize()
        {
            Client = new MobileServiceClient(ApplicationParameters.AzureAppServiceUrl);

            _auth = DependencyService.Get<IAuthentication>();

            if (!string.IsNullOrWhiteSpace(Settings.FacebookAuthToken) && !string.IsNullOrWhiteSpace(Settings.UserId))
            {
                Client.CurrentUser = new MobileServiceUser(Settings.UserId)
                {
                    MobileServiceAuthenticationToken = Settings.FacebookAuthToken
                };
            }
        }

        public async Task<bool> LoginAsync()
        {
            Initialize();

            var user = await _auth.LoginAsync(Client, MobileServiceAuthenticationProvider.Facebook);

            if (user == null)
            {
                Settings.FacebookAuthToken = string.Empty;

                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Application.Current.MainPage.DisplayAlert("Ops!", "Nao conseguimos efetuar seu login, tente novamente", "OK");
                });

                return false;
            }
            else
            {
                Settings.FacebookAuthToken = user.MobileServiceAuthenticationToken;

                //TODO: Buscar usuario da api aqui
                //Settings.UserId = user.UserId;

                _auth.RegisterPush();
            }

            return true;
        }

        public async Task LogoutAsync()
        {
            try
            {
                Settings.Clear();
                await _auth.Logout();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
        }
    }
}
