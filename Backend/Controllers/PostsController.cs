using Backend.DTOs;
using Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private IPostsService _postService;
        public PostsController(IPostsService postService) 
        {
            _postService = postService;
        }

        [HttpGet]
        public async Task<IEnumerable<PostDto>> Get() => await _postService.Get();
    }
}
