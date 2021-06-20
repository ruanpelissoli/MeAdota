using System;
using System.Collections.Generic;
using Xamarin.Forms;
using MeAdota.Mobile.Droid.Services;
using MeAdota.Mobile.Interfaces;
using Android.Webkit;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using System.Diagnostics;
using MeAdota.Mobile.Helpers;

[assembly: Dependency(typeof(AuthenticationService))]
namespace MeAdota.Mobile.Droid.Services
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
                Debug.WriteLine(ex.StackTrace);
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