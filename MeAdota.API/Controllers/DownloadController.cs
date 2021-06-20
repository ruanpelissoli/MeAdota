using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace MeAdota.API.Controllers
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
