using System;

using Foundation;
using UIKit;
using Adopcat.Mobile.Interfaces;
using Xamarin.Forms;
using Adopcat.Mobile.iOS.Services;

[assembly: Dependency(typeof(PhoneCallService))]
namespace Adopcat.Mobile.iOS.Services
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