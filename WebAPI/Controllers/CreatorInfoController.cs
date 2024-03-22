using BusinessObject.DTO;
using JwtTokenAuthorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Services;
using Swashbuckle.AspNetCore.Annotations;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreatorInfoController : ControllerBase
    {
        private readonly ICreatorInfoService _creatorInfoService;
        private ITokenHelper _tokenHelper;
        public CreatorInfoController(ICreatorInfoService creatorInfoService, ITokenHelper tokenHelper)
        {
            _creatorInfoService = creatorInfoService;
            _tokenHelper = tokenHelper;
        }

        [HttpPost("UpgradeToCreatorWithBalance")]
        [Authorize]
        [SwaggerResponse(200)]
        [SwaggerResponse(500)]
        public async Task<IActionResult> UpgradeToCreatorWithBalance([FromBody]CreatorInfoDTO creatorInfoDTO)
        {
            if (creatorInfoDTO.ContactInfo.IsNullOrEmpty() || creatorInfoDTO.Bio.IsNullOrEmpty())
            {
                return BadRequest(new() { });
            }
            var userIdString = _tokenHelper.GetUserIdFromToken(HttpContext);
            if (int.TryParse(userIdString, out var userId) == false) 
            {
                return Unauthorized(new() { });
            }
            try
            {
                await _creatorInfoService.UpgradeToCreatorWithBalance(userId, creatorInfoDTO);
                return Ok(new {});
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }
        [HttpPost("UpgradeToCreatorWithExternalParty")]
        [Authorize]
        [SwaggerResponse(200)]
        [SwaggerResponse(500)]
        public async Task<IActionResult> UpgradeToCreatorWithExternalParty([FromBody] CreatorInfoDTO creatorInfoDTO)
        {
            if (creatorInfoDTO.ContactInfo.IsNullOrEmpty() || creatorInfoDTO.Bio.IsNullOrEmpty())
            {
                return BadRequest(new() { });
            }
            var userIdString = _tokenHelper.GetUserIdFromToken(HttpContext);
            if (int.TryParse(userIdString, out var userId) == false)
            {
                return Unauthorized(new() { });
            }
            try
            {
                await _creatorInfoService.UpgradeToCreatorWithExternalParty(userId, creatorInfoDTO);
                return Ok(new {});
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
    }
}
