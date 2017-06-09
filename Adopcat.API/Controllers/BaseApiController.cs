using Adopcat.Model;
using System.Web.Http;

namespace Adopcat.API.Controllers
{
    public class BaseApiController : ApiController
    {
        public Token Token { get; set; }
    }
}