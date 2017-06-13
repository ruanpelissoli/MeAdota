using System;
using System.Collections.Generic;

using Android.App;
using Xamarin.Forms;
using Adopcat.Mobile.Droid.Services;
using Adopcat.Mobile.Interfaces;
using Android.Webkit;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Adopcat.Mobile.Droid.Helpers;
using Gcm.Client;
using System.Diagnostics;

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

        private void CreateAndShowDialog(string message, string title)
        {
            var builder = new AlertDialog.Builder(MainActivity.CurrentActivity);

            builder.SetMessage(message);
            builder.SetTitle(title);
            builder.Create().Show();
        }

        public void RegisterPush()
        {
            try
            {
                GcmClient.CheckDevice(MainActivity.CurrentActivity);
                GcmClient.CheckManifest(MainActivity.CurrentActivity);

                Debug.WriteLine("Registering...");
                GcmClient.Register(MainActivity.CurrentActivity, PushHandlerBroadcastReceiver.SENDER_IDS);
            }
            catch (Java.Net.MalformedURLException)
            {
                CreateAndShowDialog("There was an error creating the client. Verify the URL.", "Error");
            }
            catch (Exception e)
            {
                CreateAndShowDialog(e.Message, "Error");
            }
        }

        public async Task Logout()
        {
            CookieManager.Instance.RemoveAllCookie();
            await App.MobileService.LoginAsync();
        }
    }
}