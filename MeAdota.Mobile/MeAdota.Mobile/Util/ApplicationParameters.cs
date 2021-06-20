using PCLAppConfig;

namespace MeAdota.Mobile.Util
{
    public static class ApplicationParameters
    {
        public static string ApiUrl
        {
            get { return ConfigurationManager.AppSettings.Get("ApiUrl"); }
        }

        public static string AzureAppServiceUrl
        {
            get { return ConfigurationManager.AppSettings.Get("AzureAppServiceUrl"); }
        }

        public static string GoogleGeocodeApiUrl
        {
            get { return ConfigurationManager.AppSettings.Get("GoogleGeocodeApiUrl"); }
        }

        public static string GoogleGeocodeApiKey
        {
            get { return ConfigurationManager.AppSettings.Get("GoogleGeocodeApiKey"); }
        }
    }
}
