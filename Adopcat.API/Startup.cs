using Microsoft.Owin;
using Owin;
using Adopcat.API.App_Start;

[assembly: OwinStartup(typeof(Adopcat.API.Startup))]
namespace Adopcat.API
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Resolve dependencies
            SimpleInjectorWebApiInitializer.Initialize();
        }
    }
}