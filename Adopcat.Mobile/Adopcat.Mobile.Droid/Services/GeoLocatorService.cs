using System;
using Adopcat.Mobile.Interfaces;
using Adopcat.Mobile.Droid.Services;
using System.Threading.Tasks;
using Plugin.Geolocator;
using System.Net;
using Adopcat.Mobile.Util;

[assembly: Xamarin.Forms.Dependency(typeof(GeoLocatorService))]
namespace Adopcat.Mobile.Droid.Services
{
    public class GeoLocatorService : IGeoLocatorService
    {
        public async Task GetPositionInfos()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;

            var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10).Milliseconds);
            if (position == null)
                return;                 
               
        }

        private void GetGoogleMapsGeocodeData(double latitude, double longitude)
        {
            var requestUri = $"{ApplicationParameters.GoogleGeocodeApiUrl}{latitude},{longitude}&key={ApplicationParameters.GoogleGeocodeApiKey}";

            var request = WebRequest.Create(requestUri);
            var response = request.GetResponse();
        }
    }   
}