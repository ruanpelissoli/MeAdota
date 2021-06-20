using Microsoft.Owin;
using Owin;
using MeAdota.API.App_Start;
using System.Web.Http;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using MeAdota.API.Mapping;

[assembly: OwinStartup(typeof(MeAdota.API.Startup))]
namespace MeAdota.API
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Resolve dependencies
            SimpleInjectorWebApiInitializer.Initialize();

            MappingConfig.Initialize();

            // Change to camelcase (json default)
            var config = GlobalConfiguration.Configuration;
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;

            // Remove the XML formatter (we just need JSON) 
            config.Formatters.Remove(config.Formatters.XmlFormatter);

        }
    }
}