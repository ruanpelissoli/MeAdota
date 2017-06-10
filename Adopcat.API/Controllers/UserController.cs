using Adopcat.API.Models;
using Adopcat.Model;
using Adopcat.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Adopcat.API.Controllers
{
    [RoutePrefix("api/user")]
    public class UserController : BaseApiController
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
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

            await _userService.UpdateOrCreateAsync(user);

            return Ok();
        }
    }
}
