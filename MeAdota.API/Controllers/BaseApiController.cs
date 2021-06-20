using MeAdota.Model;
using System.Linq;
using System.Web.Http;

namespace MeAdota.API.Controllers
{
    public class BaseApiController : ApiController
    {
        public string Token { get; set; }

        protected void SetToken()
        {
            var token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            if (token == null || !token.Any()) return;

            Token = token.Replace("bearer ", "");
        }
    }
}