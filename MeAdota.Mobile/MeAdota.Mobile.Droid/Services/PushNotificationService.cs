using System;

using Android.App;
using MeAdota.Mobile.Interfaces;
using Xamarin.Forms;
using MeAdota.Mobile.Droid.Services;
using Gcm.Client;

[assembly: Dependency(typeof(PushNotificationService))]
namespace MeAdota.Mobile.Droid.Services
{
    public class PushNotificationService : IPushNotification
    {
        public void RegisterPush()
        {
            try
            {
                GcmClient.CheckDevice(MainActivity.CurrentActivity);
                GcmClient.CheckManifest(MainActivity.CurrentActivity);

                System.Diagnostics.Debug.WriteLine("Registering...");
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

        private void CreateAndShowDialog(string message, string title)
        {
            var builder = new AlertDialog.Builder(MainActivity.CurrentActivity);

            builder.SetMessage(message);
            builder.SetTitle(title);
            builder.Create().Show();
        }
    }
}