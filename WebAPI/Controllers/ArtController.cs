using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtController : ControllerBase
    {
        private readonly IArtService _artService;
        public ArtController(IArtService artService)
        {
            _artService = artService;
        }
        [HttpGet("GetArtList")]
        public async Task<IActionResult> GetArtList(string? searchValue, [FromQuery]List<int>tagIds, int page)
        {
            return Ok(await _artService.GetArtList(searchValue, tagIds, page));
        }
    }
}
