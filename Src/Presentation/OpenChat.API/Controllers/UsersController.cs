using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenChat.Application.Common;
using OpenChat.Application.Users;
using Exception = System.Exception;

namespace OpenChat.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
    
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
    
        [HttpPost]
        public IActionResult Create(RegistrationInputModel registrationData)
        {
            try
            {
                var createdUser = _userService.CreateUser(registrationData);
                return new CreatedResult("", createdUser);
            }
            catch (UsernameAlreadyInUseException e)
            {
                return new BadRequestObjectResult(new ApiError { Message = e.Message });
            }
        }
    }
}