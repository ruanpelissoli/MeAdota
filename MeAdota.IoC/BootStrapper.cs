using MeAdota.Data;
using MeAdota.Data.Interfaces;
using MeAdota.Data.Repository;
using MeAdota.Services;
using MeAdota.Services.Interfaces;
using SimpleInjector;
using SimpleInjector.Integration.Web;

namespace MeAdota.IoC
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
            container.Register<IPetPictureRepository, PetPictureRepository>(lifestyle);
            container.Register<IReportsRepository, ReportsRepository>(lifestyle);

            ////Services
            container.Register<IAuthenticationService, AuthenticationService>(lifestyle);
            container.Register<ILoggingService, LoggingService>(lifestyle);
            container.Register<IBlobStorageService, BlobStorageService>(lifestyle);
            container.Register<IUserService, UserService>(lifestyle);
            container.Register<IPosterService, PosterService>(lifestyle);
            container.Register<IPetPictureService, PetPictureService>(lifestyle);
            container.Register<IReportsService, ReportsService>(lifestyle);
        }
    }
}
