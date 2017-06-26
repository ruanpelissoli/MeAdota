using System;
using Android.Content;
using Adopcat.Mobile.Interfaces;
using Xamarin.Forms;
using Adopcat.Mobile.Droid.Services;

[assembly: Dependency(typeof(PhoneCallService))]
namespace Adopcat.Mobile.Droid.Services
{
    public class PhoneCallService : IPhoneCall
    {
        public async void CallNumber(string number)
        {
            try
            {
                var uri = Android.Net.Uri.Parse($"tel:{number}");
                var intent = new Intent(Intent.ActionDial, uri);
                Forms.Context.StartActivity(intent);
            }
            catch (Exception ex)
            {
                await App.ExceptionHandler.Handle(ex);
            }            
        }
    }
}