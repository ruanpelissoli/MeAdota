using System;
using System.Collections.Generic;

using Foundation;
using Adopcat.Mobile.Interfaces;
using Adopcat.Mobile.iOS.Services;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;
using Adopcat.Mobile.Helpers;

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
            foreach (var cookie in NSHttpCookieStorage.SharedStorage.Cookies)
            {
                NSHttpCookieStorage.SharedStorage.DeleteCookie(cookie);
            }
            await client.LogoutAsync();
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