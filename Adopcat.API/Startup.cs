using Microsoft.Owin;
using Owin;
using Adopcat.API.App_Start;
using System.Web.Http;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

[assembly: OwinStartup(typeof(Adopcat.API.Startup))]
namespace Adopcat.API
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Resolve dependencies
            SimpleInjectorWebApiInitializer.Initialize();

            // Change to camelcase (json default)
            var config = GlobalConfiguration.Configuration;
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            
            // Remove the XML formatter (we just need JSON) 
            config.Formatters.Remove(config.Formatters.XmlFormatter);

        }
    }
}