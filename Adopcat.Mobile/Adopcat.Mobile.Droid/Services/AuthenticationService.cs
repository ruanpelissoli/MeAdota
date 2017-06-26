using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Adopcat.Mobile.Droid.Services;
using Adopcat.Mobile.Interfaces;
using Android.Webkit;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Adopcat.Mobile.Helpers;

[assembly: Dependency(typeof(AuthenticationService))]
namespace Adopcat.Mobile.Droid.Services
{
    public class AuthenticationService : IAuthentication
    {
        public async Task<MobileServiceUser> LoginAsync(MobileServiceClient client, MobileServiceAuthenticationProvider provider, IDictionary<string, string> parameters = null)
        {
            try
            {
                var user = await client.LoginAsync(Forms.Context, provider);

                Settings.FacebookAuthToken = user?.MobileServiceAuthenticationToken ?? string.Empty;
                Settings.FacebookUserId = user?.UserId ?? string.Empty;

                return user;
            }
            catch (Exception ex)
            {
                await App.ExceptionHandler.Handle(ex);
                throw;
            }
        }

        public async Task Logout(MobileServiceClient client)
        {
            CookieManager.Instance.RemoveAllCookie();
            await client.LogoutAsync();
        }
    }
}