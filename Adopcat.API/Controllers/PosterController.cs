using Adopcat.API.Filters;
using Adopcat.API.Mapping;
using Adopcat.API.Models;
using Adopcat.Model.DTO;
using Adopcat.Services.Interfaces;
using System.Collections.Generic;
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
            var poster = await _posterService.GetAsync(id).ConfigureAwait(false);

            if (poster == null) return BadRequest();
            return Ok(poster);
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            var posters = await _posterService.GetAsync().ConfigureAwait(false);
            return Ok(MappingConfig.Mapper().Map<List<PosterOutputDTO>>(posters));
        }

        [HttpGet]
        [Route("my")]
        public async Task<IHttpActionResult> GetMyPosters(int userId)
        {
            return Ok(await _posterService.GetByUserIdAsync(userId).ConfigureAwait(false));
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post(PosterInputDTO model)
        {
            return Ok(await _posterService.CreateAsync(model).ConfigureAwait(false));
        }
    }
}
