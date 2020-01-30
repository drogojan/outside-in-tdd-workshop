using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OpenChat.Application.Common;
using OpenChat.Application.Users;

namespace OpenChat.API.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            this._loginService = loginService;
        }

        [HttpPost]
        public IActionResult Post(UserCredentials userCredentials)
        {
            try {
                UserApiModel loggedInUser = _loginService.Login(userCredentials);
                return base.Ok(loggedInUser);
            } catch (InvalidCredentialsException e) {
                return new NotFoundObjectResult(new ApiError { Message = e.Message });
            }
        }
    }
}