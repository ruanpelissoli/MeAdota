using Adopcat.API.Filters;
using Adopcat.Services.Interfaces;
using System.Threading.Tasks;
using System.Web.Http;

namespace Adopcat.API.Controllers
{
    [CustomAuthorize]
    [RoutePrefix("api/poster")]
    public class PosterController : ApiController
    {
        private IPosterService _posterService;

        public PosterController(IPosterService posterService)
        {
            _posterService = posterService;
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get(int id)
        {
            var poster = await  _posterService.GetAsync(id);

            if (poster == null) return BadRequest();
            return Ok(poster);
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            return Ok(await _posterService.GetAsync().ConfigureAwait(false));
        }
    }
}
