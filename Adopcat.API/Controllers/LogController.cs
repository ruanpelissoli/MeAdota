using Adopcat.API.Filters;
using Adopcat.Model;
using Adopcat.Services.Interfaces;
using System.Threading.Tasks;
using System.Web.Http;

namespace Adopcat.API.Controllers
{
    [CustomAuthorize]
    [RoutePrefix("api/log")]
    public class LogController : ApiController
    {
        private ILoggingService _loggingService;
        
        public LogController(ILoggingService loggingService)
        {
            _loggingService = loggingService;
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post(SystemLog log)
        {
            await _loggingService.CreateAsync(log);
            return Ok();
        }
    }
}
