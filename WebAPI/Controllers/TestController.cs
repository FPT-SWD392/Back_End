using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class TestController : Controller
    {
        [HttpGet("Test")]
        public IActionResult Test()
        {
            return Ok("Your api has been successfully deployed");
        }
    }
}
