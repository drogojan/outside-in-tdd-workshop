using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenChat.Application.Common;
using OpenChat.Application.Users;

namespace OpenChat.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        public IActionResult RegisterUser(UserRegistration userRegistration)
        {
            try
            {
                var registeredUser = userService.CreateUser(userRegistration);
                return new CreatedResult("", registeredUser);
            }
            catch (UsernameAlreadyInUseException e)
            {
                return new BadRequestObjectResult(new ApiError { Message = e.Message });
            }
        }
    }
}