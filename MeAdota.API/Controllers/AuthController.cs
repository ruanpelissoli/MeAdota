using MeAdota.API.Filters;
using MeAdota.API.Models;
using MeAdota.Services.Interfaces;
using System.Threading.Tasks;
using System.Web.Http;

namespace MeAdota.API.Controllers
{
    [RoutePrefix("api/auth")]
    public class AuthController : BaseApiController
    {
        private IAuthenticationService _authenticationService;
        private IUserService _userService;

        public AuthController(IAuthenticationService authenticationService, IUserService userService)
        {
            _authenticationService = authenticationService;
            _userService = userService;
        }

        [Route("login")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IHttpActionResult> Login(LoginViewModel loginModel)
        {
            var token = await _authenticationService.GenerateToken(loginModel.Email, loginModel.Password);
            var userId = _authenticationService.GetByAccessToken(token).UserId;

            var loginResponse = new LoginResponseViewModel
            {
                AuthToken = token,
                UserId = userId
            };

            return Ok(loginResponse);
        }

        [Route("login/fb")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IHttpActionResult> LoginFacebook(LoginViewModel loginModel)
        {
            var token = await _authenticationService.GenerateTokenByFacebook(loginModel.Email, loginModel.Password);
            var userId = _authenticationService.GetByAccessToken(token).UserId;

            var loginResponse = new LoginResponseViewModel
            {
                AuthToken = token,
                UserId = userId
            };

            return Ok(loginResponse);
        }

        [Route("logout")]
        [HttpGet]
        [CustomAuthorize]
        public async Task<IHttpActionResult> Logout()
        {
            var authToken = _authenticationService.GetByAccessToken(Token);
            await _authenticationService.KillToken(authToken.Id);
            return Ok();
        }
    }
}
