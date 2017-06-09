using Adopcat.API.Filters;
using Adopcat.API.Models;
using Adopcat.Services.Interfaces;
using System.Web.Http;

namespace Adopcat.API.Controllers
{
    [RoutePrefix("api/auth")]
    public class AuthController : BaseApiController
    {
        private IAuthenticationService _authenticationService;

        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [Route("login")]
        [HttpPost]
        [AllowAnonymous]
        public IHttpActionResult Login(LoginViewModel loginModel)
        {
            return Ok(_authenticationService.GenerateToken(loginModel.Email, loginModel.Password));
        }

        [Route("Logout")]
        [HttpGet]
        [CustomAuthorize]
        public IHttpActionResult Logout()
        {
            _authenticationService.KillToken(this.Token.Id);
            return Ok();
        }

    }
}
