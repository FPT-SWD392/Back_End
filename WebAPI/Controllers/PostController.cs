using BusinessObject.SqlObject;
using JwtTokenAuthorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : Controller
    {
        private readonly ITokenHelper _jwtHelper;
        private readonly IUserInfoService _userInfo;
        private readonly IPostService _postService;

        public PostController(ITokenHelper jwtHelper, IUserInfoService userInfo, IPostService postService)
        {
            _jwtHelper = jwtHelper;
            _userInfo = userInfo;
            _postService = postService;
        }

        //[Authorize]
        [HttpPost("CreatePost")]
        public async Task<IActionResult> CreatePost([FromBody] Post post)
        {
            await _postService.CreatePost(post);
            return Ok(new {});
        }

        //[Authorize]
        [HttpGet("GetAllPosts")]
        public async Task<IActionResult> GetAllPosts()
        {
            var posts = await _postService.GetAllPosts();
            return Ok(posts);
        }

        //[Authorize]
        [HttpGet("GetPostById/{postId}")]
        public async Task<IActionResult> GetPostById(int postId)
        {
            var post = await _postService.GetPostById(postId);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }

        //[Authorize]
        [HttpPost("UpdatePost")]
        public async Task<IActionResult> UpdatePost([FromBody] Post updatedPost)
        {
            await _postService.UpdatePost(updatedPost);
            return NoContent();
        }

        //[Authorize]
        [HttpDelete("Delete/{postId}")]
        public async Task<IActionResult> DeletePost(int postId)
        {
            await _postService.DeletePost(postId);
            return NoContent();
        }
    }
}
