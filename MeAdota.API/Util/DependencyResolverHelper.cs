using System.Web.Http;

namespace MeAdota.API.Util
{
    public class DependencyResolverHelper
    {
        public static T GetService<T>()
        {
            return (T)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(T));
        }
    }
}