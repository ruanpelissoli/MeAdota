using Adopcat.Mobile.Models;
using Adopcat.Mobile.Services;
using Adopcat.Mobile.Util;
using Newtonsoft.Json;
using Plugin.Geolocator;
using System;
using System.Linq;
using System.Net;
using Xamarin.Forms;

[assembly: Dependency(typeof(GeoLocatorService))]
namespace Adopcat.Mobile.Services
{
    public class GeoLocatorService
    {
        public async void GetLocation()
        {
            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;

                var position = await locator.GetPositionAsync();
                if (position == null)
                    return;

                var mapsApiUrl = ApplicationParameters.GoogleGeocodeApiUrl;

                var request = (HttpWebRequest)WebRequest.Create(string.Format(mapsApiUrl + $"&key={ApplicationParameters.GoogleGeocodeApiKey}", position.Latitude, position.Longitude));
                request.BeginGetResponse(new AsyncCallback(GeoCodeApiResponse), request);
            }
            catch (Exception ex)
            {
                await App.ExceptionHandler.Handle(ex);
            }
        }

        private void GeoCodeApiResponse(IAsyncResult result)
        {
            if (result != null)
            {
                HttpWebResponse response = (result.AsyncState as HttpWebRequest).EndGetResponse(result) as HttpWebResponse;

                using (var reader = new System.IO.StreamReader(response.GetResponseStream()))
                {
                    string responseText = reader.ReadToEnd();

                    var googleGeoCodeResponse = JsonConvert.DeserializeObject<GoogleGeoCodeResponse>(responseText);

                    var address = googleGeoCodeResponse.results[1].formatted_address;
                }
            }
        }
    }
}

