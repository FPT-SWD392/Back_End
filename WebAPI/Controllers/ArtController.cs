﻿using BusinessObject;
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
        private readonly ITokenHelper _tokenHelper;
        public ArtController(IArtService artService, ITagService tagService, ITokenHelper tokenHelper)
        {
            _artService = artService;
            _tagService = tagService;
            _tokenHelper = tokenHelper;
        }
        [HttpGet("GetArtList")]
        [Produces("application/json")]
        [SwaggerResponse(200, Type = typeof(List<ArtworkPreviewDTO>))]
        [SwaggerResponse(400)]
        public async Task<IActionResult> GetArtList(string? searchValue, [FromQuery] List<int> tagIds, int page)
        {
            if (page < 1)
            {
                return BadRequest("Page number must be larger than 0");
            }
            return Ok(await _artService.GetArtList(searchValue, tagIds, page));
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

    }
}
