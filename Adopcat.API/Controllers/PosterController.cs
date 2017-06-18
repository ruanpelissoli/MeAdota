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
            return Ok(MappingConfig.Mapper().Map<PosterOutputDTO>(poster));
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAllPosters(int userId)
        {
            var posters = await _posterService.GetAllPostersAsync(userId).ConfigureAwait(false);
            return Ok(MappingConfig.Mapper().Map<List<PosterOutputDTO>>(posters));
        }

        [HttpGet]
        [Route("filter")]
        public async Task<IHttpActionResult> GetAllPosters(int userId, FilterDTO filter)
        {
            var posters = await _posterService.GetAllPostersAsync(userId).ConfigureAwait(false);
            return Ok(MappingConfig.Mapper().Map<List<PosterOutputDTO>>(posters));
        }

        [HttpGet]
        [Route("my")]
        public async Task<IHttpActionResult> GetMyPosters(int userId)
        {
            var posters = await _posterService.GetByUserIdAsync(userId).ConfigureAwait(false);
            return Ok(MappingConfig.Mapper().Map<List<PosterOutputDTO>>(posters));
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post(PosterInputDTO model)
        {
            var poster = await _posterService.CreateAsync(model).ConfigureAwait(false);
            return Ok(MappingConfig.Mapper().Map<PosterOutputDTO>(poster));
        }
    }
}
