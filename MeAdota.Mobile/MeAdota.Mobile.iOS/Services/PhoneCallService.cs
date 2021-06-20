using System;

using Foundation;
using UIKit;
using MeAdota.Mobile.Interfaces;
using Xamarin.Forms;
using MeAdota.Mobile.iOS.Services;

[assembly: Dependency(typeof(PhoneCallService))]
namespace MeAdota.Mobile.iOS.Services
{
    public class PhoneCallService : IPhoneCall
    {
        public void CallNumber(string number)
        {
            try
            {
                var url = new NSUrl($"tel:{number}");
                UIApplication.SharedApplication.OpenUrl(url);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}