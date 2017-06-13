using PCLAppConfig;

namespace Adopcat.Mobile.Util
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
    }
}
