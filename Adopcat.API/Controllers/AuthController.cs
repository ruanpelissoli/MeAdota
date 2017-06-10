using Adopcat.API.Filters;
using Adopcat.API.Models;
using Adopcat.Services.Interfaces;
using System.Threading.Tasks;
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
        public async Task<IHttpActionResult> Login(LoginViewModel loginModel)
        {
            return Ok(await _authenticationService.GenerateToken(loginModel.Email, loginModel.Password));
        }

        [Route("logout")]
        [HttpGet]
        [CustomAuthorize]
        public async Task<IHttpActionResult> Logout()
        {
            await _authenticationService.KillToken(Token.Id);
            return Ok();
        }
    }
}
