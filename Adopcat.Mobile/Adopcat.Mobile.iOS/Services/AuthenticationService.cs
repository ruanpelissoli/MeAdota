using System;
using System.Collections.Generic;

using Foundation;
using Adopcat.Mobile.Interfaces;
using Adopcat.Mobile.iOS.Services;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;
using Adopcat.Mobile.iOS.Helpers;
using System.Diagnostics;

[assembly: Xamarin.Forms.Dependency(typeof(AuthenticationService))]
namespace Adopcat.Mobile.iOS.Services
{
    class AuthenticationService : IAuthentication
    {
        public async Task<MobileServiceUser> LoginAsync(MobileServiceClient client, MobileServiceAuthenticationProvider provider, IDictionary<string, string> parameters = null)
        {
            try
            {
                var current = GetController();
                var user = await client.LoginAsync(current, provider);

                Settings.AuthToken = user?.MobileServiceAuthenticationToken ?? string.Empty;
                Settings.UserId = user?.UserId ?? string.Empty;

                return user;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                throw;
            }
        }

        public async Task Logout()
        {
            foreach (var cookie in NSHttpCookieStorage.SharedStorage.Cookies)
            {
                NSHttpCookieStorage.SharedStorage.DeleteCookie(cookie);
            }
            await App.MobileService.LoginAsync();
        }

        public void RegisterPush() { }

        private UIKit.UIViewController GetController()
        {
            var window = UIKit.UIApplication.SharedApplication.KeyWindow;
            var root = window.RootViewController;

            if (root == null) return null;

            var current = root;
            while (current.PresentedViewController != null)
            {
                current = current.PresentedViewController;
            }

            return current;
        }
    }
}