using System;
using Android.Content;
using MeAdota.Mobile.Interfaces;
using Xamarin.Forms;
using MeAdota.Mobile.Droid.Services;

[assembly: Dependency(typeof(PhoneCallService))]
namespace MeAdota.Mobile.Droid.Services
{
    public class PhoneCallService : IPhoneCall
    {
        public void CallNumber(string number)
        {
            try
            {
                var uri = Android.Net.Uri.Parse($"tel:{number}");
                var intent = new Intent(Intent.ActionDial, uri);
                Forms.Context.StartActivity(intent);
            }
            catch (Exception)
            {
                throw;
            }            
        }
    }
}