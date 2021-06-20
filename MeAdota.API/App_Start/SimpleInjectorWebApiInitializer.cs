using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Integration.Web.Mvc;
using SimpleInjector.Lifestyles;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Microsoft.Owin;
using MeAdota.IoC;

namespace MeAdota.API.App_Start
{
    public class SimpleInjectorWebApiInitializer
    {
        public static Container Container { get; private set; }

        // Initialize the container and register it as Web API Dependency Resolver
        public static void Initialize()
        {
            Container = new Container();
            Container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            InitializeContainer(Container);
            Container.RegisterSingleton(() =>
            {
                if (HttpContext.Current != null && HttpContext.Current.Items["owin.Environment"] == null && Container.IsVerifying)
                    return new OwinContext().Authentication;
                return HttpContext.Current.GetOwinContext().Authentication;
            });
            Container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
            Container.RegisterMvcControllers(System.Reflection.Assembly.GetExecutingAssembly());
            Container.RegisterMvcIntegratedFilterProvider();

            Container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(Container);
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(Container));
        }

        private static void InitializeContainer(Container container)
        {
            BootStrapper.RegisterServices(container);
        }
    }
}