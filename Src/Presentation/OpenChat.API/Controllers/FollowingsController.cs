using System;
using Microsoft.AspNetCore.Mvc;
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
            followingService.CreateFollowing(following);
            return new EmptyResult();
        }
    }
}