using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using OpenChat.Application.Posts;
using OpenChat.Application.Wall;

namespace OpenChat.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class WallController : ControllerBase
    {
        private IWallService wallService;

        public WallController(IWallService wallService)
        {
            this.wallService = wallService;
        }

        [HttpGet]
        [Route("{userId}/wall")]
        public IEnumerable<PostApiModel> WallPosts(Guid userId)
        {
            return wallService.GetWallPosts(userId);
        }
    }
}