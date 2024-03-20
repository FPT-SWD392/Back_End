using BusinessObject;
using BusinessObject.DTO;
using BusinessObject.SqlObject;
using JwtTokenAuthorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtController : ControllerBase
    {
        private readonly IArtService _artService;
        private readonly ITagService _tagService;
        private readonly IPurchaseService _purchaseService;
        private readonly IArtRatingService _artRatingService;
        private readonly ITokenHelper _tokenHelper;
        public ArtController(IArtService artService, ITagService tagService, ITokenHelper tokenHelper, IArtRatingService artRatingService)
        {
            _artService = artService;
            _tagService = tagService;
            _tokenHelper = tokenHelper;
            _artRatingService = artRatingService;
        }
        [HttpGet("GetArtList")]
        [Produces("application/json")]
        [SwaggerResponse(200, Type = typeof(ArtworkListDTO))]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
        public async Task<IActionResult> GetArtList(string? searchValue, [FromQuery] List<int> tagIds, int page)
        {
            if (page < 1)
            {
                return BadRequest("Page number must be larger than 0");
            }
            try
            {
                string userIdString = _tokenHelper.GetUserIdFromToken(HttpContext);
                int userId = int.Parse(userIdString);
                return Ok(await _artService.GetArtListForLoggedUser(userId, searchValue, tagIds, page));
            } catch
            {
                return Ok(await _artService.GetArtList(searchValue, tagIds, page));
            }
        }
        [HttpGet("GetPurchasedArtList")]
        [Produces("application/json")]
        [SwaggerResponse(200, Type = typeof(ArtworkListDTO))]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
        public async Task<IActionResult> GetPurchasedArtList(string? searchValue, [FromQuery] List<int> tagIds, int page)
        {
            if (page < 1)
            {
                return BadRequest("Page number must be larger than 0");
            }
            try
            {
                string userIdString = _tokenHelper.GetUserIdFromToken(HttpContext);
                int userId = int.Parse(userIdString);
                return Ok(await _artService.GetPurchasedArtList(userId,searchValue,tagIds,page));
            }
            catch
            {
                return Unauthorized();
            }
        }
        [HttpGet("GetCreatedArtList")]
        [Produces("application/json")]
        [SwaggerResponse(200, Type = typeof(ArtworkListDTO))]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
        public async Task<IActionResult> GetCreatedArtList(string? searchValue, [FromQuery] List<int> tagIds, int page)
        {
            if (page < 1)
            {
                return BadRequest("Page number must be larger than 0");
            }
            try
            {
                string creatorIdString = _tokenHelper.GetCreatorIdFromToken(HttpContext);
                int creatorId = int.Parse(creatorIdString);
                return Ok(await _artService.GetCreatedArtList(creatorId, searchValue, tagIds, page));
            }
            catch
            {
                return Unauthorized();
            }
        }
        [HttpGet("GetArtInfo")]
        [Produces("application/json")]
        [SwaggerResponse(200, Type = typeof(ArtworkDetailDTO))]
        [SwaggerResponse(400)]
        public async Task<IActionResult> GetArtDetails(int artId)
        {
            ArtworkDetailDTO? artwork = await _artService.GetArtDetails(artId);
            if (artwork == null) return NotFound();
            return Ok(artwork);
        }
        [HttpGet("GetCreatorArtInfo")]
        [Produces("application/json")]
        [SwaggerResponse(200, Type = typeof(ArtworkListDTO))]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
        public async Task<IActionResult> GetCreatorsArtDetails(int artId)
        {
            try
            {
                int creatorId = int.Parse(_tokenHelper.GetCreatorIdFromToken(HttpContext));
                ArtworkDetailDTO? artwork = await _artService.GetArtDetails(artId,creatorId);
                if (artwork == null) return NotFound();
                return Ok(artwork);
            } catch
            {
                return Unauthorized();
            }
        }
        [HttpGet("GetAllArtTags")]
        [Produces("application/json")]
        [SwaggerResponse(200, Type = typeof(List<Tag>))]
        public async Task<IActionResult> GetArtTags()
        {
            return Ok(await _tagService.GetTags());
        }
        [HttpPost("CreateArtwork")]
        [Authorize]
        [SwaggerResponse(200)]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
        public async Task<IActionResult> CreateArtwork([FromForm] CreateArtRequest createArtRequest)
        {
            try
            {
                string creatorIdString = _tokenHelper.GetCreatorIdFromToken(HttpContext);
                if (false == int.TryParse(creatorIdString, out int creatorId)) return Unauthorized();
                if (createArtRequest == null) return BadRequest();
                if (false == ImageType.IsSupportedImageType(createArtRequest.ImageFile.ContentType)) return BadRequest("Unsupported image file type");
                await _artService.CreateArt(creatorId, createArtRequest);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
        [HttpGet("Preview")]
        [SwaggerResponse(200)]
        [SwaggerResponse(404)]
        public async Task<IActionResult> DownloadPreview(int artId)
        {
            ImageDTO? imageDTO = await _artService.DownloadPreview(artId);
            if (imageDTO == null) return NotFound();
            return File(imageDTO.FileStream, imageDTO.ImageType, enableRangeProcessing: true);
        }
        [HttpGet("Download")]
        [Authorize]
        [SwaggerResponse(200)]
        [SwaggerResponse(404)]
        public async Task<IActionResult> DownloadOriginal(int artId)
        {
            string userIdString = _tokenHelper.GetUserIdFromToken(HttpContext);
            if (false == int.TryParse(userIdString, out var userId))
            {
                return Unauthorized();
            }
            ImageDTO? imageDTO = await _artService.DownloadOriginal(userId, artId);
            if (imageDTO == null) return NotFound();
            return File(imageDTO.FileStream, imageDTO.ImageType, enableRangeProcessing: true);
        }
        [HttpPost("PurchaseWithBalance/{artId}")]
        [Authorize]
        [SwaggerResponse(200)]
        [SwaggerResponse(400, "Already bought || Don't have enough balance")]
        [SwaggerResponse(500)]
        public async Task<IActionResult> PurchaseWithBalance(int artId)
        {
            string userIdString = _tokenHelper.GetUserIdFromToken(HttpContext);
            if (false == int.TryParse(userIdString, out var userID))
            {
                return Unauthorized();
            }
            bool checkSuccess = false;
            try
            {
                checkSuccess = await _purchaseService.PurchaseWithBalance(userID, artId);
            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            
            if (checkSuccess)
            {
                return Ok();
            }
            else
            {
                return BadRequest("You have already bought this art/You don't have enough balance");
            }        
        }
        [HttpPost("PurchaseWithExternalParty/{artId}")]
        [Authorize]
        [SwaggerResponse(200)]
        [SwaggerResponse(400, "Already bought")]
        public async Task<IActionResult> PurchaseWithExternalParty(int artId)
        {
            string userIdString = _tokenHelper.GetUserIdFromToken(HttpContext);
            if (false == int.TryParse(userIdString, out var userID))
            {
                return Unauthorized();
            }

            bool checkSuccess = false;
            try
            {
                checkSuccess = await _purchaseService.PurchaseWithExternalParty(userID, artId);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            return Ok();
        }
        [HttpPost("Rating")]
        [Authorize]
        [SwaggerResponse(200)]
        [SwaggerResponse(400, "Already rated")]
        public async Task<IActionResult> RatingArtwork([FromBody] RatingDTO ratingDTO)
        {
            if (ratingDTO.Rating == null || ratingDTO.Rating < 1 || ratingDTO.Rating > 5)
            {
                return BadRequest("Rating does not exist in this request");
            }
            string userIdString = _tokenHelper.GetUserIdFromToken(HttpContext);
            if (false == int.TryParse(userIdString, out var userId))
            {
                return Unauthorized("");
            }

            bool checkSuccess = await _artRatingService.RatingArtwork(userId, ratingDTO.ArtId, ratingDTO.Rating);
            if (checkSuccess)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
