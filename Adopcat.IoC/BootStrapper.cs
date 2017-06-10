using Adopcat.Data;
using Adopcat.Data.Interfaces;
using Adopcat.Data.Repository;
using Adopcat.Services;
using Adopcat.Services.Interfaces;
using SimpleInjector;
using SimpleInjector.Integration.Web;

namespace Adopcat.IoC
{
    public class BootStrapper
    {
        public static void RegisterServices(Container container, bool shouldRegisterOwin = false, bool shouldBeSingleton = false)
        {
            var lifestyle = shouldBeSingleton ? Lifestyle.Singleton : new WebRequestLifestyle();

            container.RegisterSingleton<DatabaseContext>();
            container.Register<IDbContextFactory, DatabaseContextFactory>(lifestyle);
            
            //// Repositories
            container.Register<IApplicationParameterRepository, ApplicationParameterRepository>(lifestyle);
            container.Register<ISystemLogRepository, SystemLogRepository>(lifestyle);
            container.Register<IUserRepository, UserRepository>(lifestyle);
            container.Register<ITokenRepository, TokenRepository>(lifestyle);
            container.Register<IPosterRepository, PosterRepository>(lifestyle);

            ////Services
            container.Register<IAuthenticationService, AuthenticationService>(lifestyle);
            container.Register<ILoggingService, LoggingService>(lifestyle);
            //container.Register<IBlobStorageService, BlobStorageService>(lifestyle);
            container.Register<IUserService, UserService>(lifestyle);
            container.Register<IPosterService, PosterService>(lifestyle);
        }
    }
}
