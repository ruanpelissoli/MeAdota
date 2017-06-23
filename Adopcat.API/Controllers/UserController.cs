using Adopcat.API.Filters;
using Adopcat.API.Models;
using Adopcat.Model;
using Adopcat.Services.Interfaces;
using Adopcat.Services.Util;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace Adopcat.API.Controllers
{
    [RoutePrefix("api/user")]
    public class UserController : BaseApiController
    {
        private IUserService _userService;
        private IBlobStorageService _blobService;

        public UserController(IUserService userService, IBlobStorageService blobService)
        {
            _userService = userService;
            _blobService = blobService;
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get(int id)
        {
            return Ok(await _userService.GetById(id));
        }

        [HttpGet]
        [Route("byemail")]
        public async Task<IHttpActionResult> GetByEmail(string email)
        {
            return Ok(await _userService.GetByEmail(email));
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post(UserViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = new User
            {
                Email = model.Email,
                Name = model.Name,
                Password = model.Password,
                Phone = model.Phone
            };

            user.PictureUrl = await _blobService.AddUserImageToStorageAsync(model.Picture);

            user = await _userService.UpdateOrCreateAsync(user);

            return Ok(user);
        }

        [HttpPost]
        [Route("fb")]
        public async Task<IHttpActionResult> CreateFacebookUser(FacebookUserViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = new User
            {
                Email = model.Email,
                FacebookId = model.FacebookId,
                Name = model.Name,
                PictureUrl = model.PictureUrl,
                Phone = model.Phone
            };

            user = await _userService.UpdateOrCreateAsync(user);

            return Ok(user);
        }

        [HttpPut]
        [CustomAuthorize]
        public async Task<IHttpActionResult> Update(string email, UserViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            if (token == null || !token.Any()) return Unauthorized();

            var user = await _userService.GetByToken(token.Replace("bearer ", ""));

            if (user == null || !user.Email.Equals(email)) return Unauthorized();

            if (!string.IsNullOrEmpty(user.Password) && !user.Password.Equals(model.Password))
            {
                user.Password = Cryptography.GetMD5Hash(model.Password);
            }

            if(model.Picture != null)
            {
                await _blobService.DeleteBlobStorageAsync(model.PictureUrl);
                user.PictureUrl = await _blobService.AddUserImageToStorageAsync(model.Picture);
            }
            
            user.Phone = model.Phone;
            user.ReceiveNotifications = model.ReceiveNotifications;
            user.RegistrationId = model.RegistrationId;

            await _userService.Update(user);

            return Ok();
        }
    }
}
