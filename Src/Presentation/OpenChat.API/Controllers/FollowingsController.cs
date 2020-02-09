using System;
using Microsoft.AspNetCore.Mvc;
using OpenChat.Application.Common;
using OpenChat.Application.Followings;

namespace OpenChat.API.Controllers
{
    [Route("api/followings")]
    [ApiController]
    public class FollowingsController : ControllerBase
    {
        private IFollowingService followingService;

        public FollowingsController(IFollowingService followingService)
        {
            this.followingService = followingService;
        }

        public IActionResult Create(FollowingInputModel following)
        {
            try {
                followingService.CreateFollowing(following);
                return new CreatedResult("", null);
            } catch (FollowingAlreadyExistsException ex) {
                return new BadRequestObjectResult(new ApiError { Message = ex.Message });
            }
        }
    }
}