using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace Adopcat.Mobile.Helpers
{
    public static class Settings
    {
        private static ISettings AppSettings => CrossSettings.Current;

        const string UserIdKey = "userid";
        static readonly string UserIdDefault = string.Empty;

        const string AuthTokenKey = "authtoken";
        static readonly string AuthTokenDefault = string.Empty;

        const string FacebookUserIdKey = "facebookuserid";
        static readonly string FacebookUserIdDefault = string.Empty;

        const string FacebookAuthTokenKey = "facebookauthtoken";
        static readonly string FacebookAuthTokenDefault = string.Empty;

        public static string AuthToken
        {
            get
            {
                return AppSettings.GetValueOrDefault(AuthTokenKey, AuthTokenDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(AuthTokenKey, value);
            }
        }

        public static string UserId
        {
            get
            {
                return AppSettings.GetValueOrDefault(UserIdKey, UserIdDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(UserIdKey, value);
            }
        }

        public static string FacebookUserId
        {
            get
            {
                return AppSettings.GetValueOrDefault(FacebookUserIdKey, FacebookUserIdDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(FacebookUserIdKey, value);
            }
        }

        public static string FacebookAuthToken
        {
            get
            {
                return AppSettings.GetValueOrDefault(FacebookAuthTokenKey, FacebookAuthTokenDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(FacebookAuthTokenKey, value);
            }
        }
        
        public static bool IsLoggedIn => !string.IsNullOrEmpty(AuthToken);

        public static void Clear()
        {
            AuthToken = null;
            UserId = null;
            FacebookUserId = null;
            FacebookAuthToken = null;
        }
    }    
}