using System.Net.Http;

namespace Adopcat.API.Extensions
{
    public static class RequestExtension
    {
        public static string GetCurrentBearerAuthrorizationToken(this HttpRequestMessage request)
        {
            string authToken = null;
            if (request.Headers.Authorization != null)
            {
                if (request.Headers.Authorization.Scheme.ToLower() == "bearer")
                {
                    authToken = request.Headers.Authorization.Parameter;
                }
            }
            return authToken;
        }
    }
}