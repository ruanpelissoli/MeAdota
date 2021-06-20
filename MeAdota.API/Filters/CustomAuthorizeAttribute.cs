using MeAdota.API.Controllers;
using MeAdota.API.Extensions;
using MeAdota.API.Util;
using MeAdota.Services.Interfaces;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace MeAdota.API.Filters
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var authToken = actionContext.ControllerContext.Request.GetCurrentBearerAuthrorizationToken();
            var authenticationService = DependencyResolverHelper.GetService<IAuthenticationService>();
            var token = authenticationService.GetByAccessToken(authToken);
            if (token != null)
            {
                if (actionContext.ControllerContext.Controller.GetType().IsSubclassOf(typeof(BaseApiController)))
                {
                    if (((BaseApiController)actionContext.ControllerContext.Controller).Token == null && actionContext.ActionDescriptor.ActionName != "Logout")
                        authenticationService.RefreshToken(token);
                    ((BaseApiController)actionContext.ControllerContext.Controller).Token = token.Access_token;
                }
                else
                    authenticationService.RefreshToken(token);
            }

            if (SkipAuthorization(actionContext))
            {
                return;
            }

            if (token == null || token.User == null)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized) { Content = new StringContent("Unauthorized") };
            }
        }

        private static bool SkipAuthorization(HttpActionContext actionContext)
        {
            return actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any()
                   || actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any();
        }
    }
}