using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OpenChat.Application.Common;
using OpenChat.Application.Posts;

namespace OpenChat.API.Controllers
{
    [Route("api/users/{userId}/timeline")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private IPostService postService;

        public PostsController(IPostService postService)
        {
            this.postService = postService;
        }

        public IActionResult Create(Guid userId, NewPost post)
        {
            try {
                return new CreatedResult("", this.postService.CreatePost(userId, post.Text));
            } catch (InappropriateLanguageException ex) {
                return new BadRequestObjectResult(new ApiError { Message = ex.Message  });
            }
        }
    }
}