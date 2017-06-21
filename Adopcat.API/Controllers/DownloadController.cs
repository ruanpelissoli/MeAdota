using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace Adopcat.API.Controllers
{
    [RoutePrefix("api/download")]
    public class DownloadController : BaseApiController
    {
        [HttpGet]
        public async Task<IHttpActionResult> Get(string url)
        {
            var webClient = new WebClient();
            return Ok(await webClient.DownloadDataTaskAsync(url));
        }
    }
}
