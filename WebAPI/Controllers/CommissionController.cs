using BusinessObject;
using BusinessObject.DTO;
using BusinessObject.SqlObject;
using JwtTokenAuthorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using Swashbuckle.AspNetCore.Annotations;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommissionController : ControllerBase
    {
        private readonly ITokenHelper _jwtHelper;
        private readonly ICommissionService _commissionService;
        private readonly IUserInfoService _userInfoService;

        public CommissionController(ITokenHelper jwtHelper, ICommissionService commissionService,
            IUserInfoService userInfoService)
        {
            _jwtHelper = jwtHelper;
            _commissionService = commissionService;
            _userInfoService = userInfoService;
        }

        [Authorize]
        [HttpPut("CreateCommission")]
        [SwaggerResponse(200, Type = typeof(object))]
        [SwaggerResponse(400, "Deadline < Now error, or balance error", Type = typeof(string))]
        [SwaggerResponse(401, Type = typeof(object))]
        public async Task<IActionResult> CreateCommission([FromBody] CreateCommissionRequest commissionRequest)
        {
            if (commissionRequest.Deadline <= DateTime.Today)
            {
                return BadRequest("Deadline can not be today");
            }
            if (false == int.TryParse(_jwtHelper.GetUserIdFromToken(HttpContext), out int userId))
            {
                return Unauthorized(new object());
            }
            (bool isCreated, string message) = await _commissionService.CreateCommission(commissionRequest.Deadline, commissionRequest.Price, commissionRequest.CreatorId, userId);
            if (isCreated)
            {
                return Ok(new object());
            }
            return BadRequest(new
            {
                Error = isCreated
            });
        }

        [Authorize]
        [HttpPut("CancelCommission")]
        [SwaggerResponse(200)]
        [SwaggerResponse(400, Type = typeof(string))]
        public async Task<IActionResult> UserCancelCommission(int commissionId)
        {
            if (int.TryParse(_jwtHelper.GetUserIdFromToken(HttpContext), out int userId))
            {
                return Unauthorized(new object());
            }
            var (isUpdated, message) = await _commissionService.CancelCommission(userId, commissionId);
            if (isUpdated)
            {
                return Ok(new object());
            }
            return BadRequest(new
            {
                Error = message
            });
        }

        [Authorize]
        [HttpPut("Creator/AcceptCommission")]
        [SwaggerResponse(200)]
        [SwaggerResponse(400, Type = typeof(string))]
        public async Task<IActionResult> AcceptCommission(int commissionId)
        {
            if (false == int.TryParse(_jwtHelper.GetCreatorIdFromToken(HttpContext), out int creatorId))
            {
                return Unauthorized(new object());
            }
            var (isUpdated, message) = await _commissionService.AcceptCommission(creatorId, commissionId);
            if (isUpdated)
            {
                return Ok(new object());
            }
            return BadRequest(new
            {
                Error = message
            });
        }

        [Authorize]
        [HttpPut("Creator/DenyCommission")]
        [SwaggerResponse(200)]
        [SwaggerResponse(400, Type = typeof(string))]
        public async Task<IActionResult> DenyCommission(int commissionId)
        {
            if (false == int.TryParse(_jwtHelper.GetCreatorIdFromToken(HttpContext), out int creatorId))
            {
                return Unauthorized(new object());
            }
            var (isUpdated, message) = await _commissionService.DenyCommission(creatorId, commissionId);
            if (isUpdated)
            {
                return Ok(new object());
            }
            return BadRequest(new
            {
                Error = message
            });
        }

        [Authorize]
        [HttpGet("Creator/GetAcceptedCommission")]
        [SwaggerResponse(200)]
        [SwaggerResponse(400, Type = typeof(string))]
        public async Task<IActionResult> GetArtistAcceptedCommission()
        {
            if (false == int.TryParse(_jwtHelper.GetCreatorIdFromToken(HttpContext), out int artistId))
            {
                return Unauthorized(new object());
            }
            List<Commission> commission = await _commissionService.GetCreatorAcceptedCommissions(artistId);
            List<ViewCommissionResponse> response = new();
            foreach (Commission item in commission)
            {
                ViewCommissionResponse r = new()
                {
                    CommisionId = item.CommissionId,
                    CreatedDate = item.CreatedDate,
                    Deadline = item.Deadline,
                    Price = item.Price,
                    UserName = item.UserInfo.NickName,
                    CommissionStatus = item.CommissionStatus,
                    Description = item.Description,
                };
                response.Add(r);
            }
            return Ok(response);
        }

        [Authorize]
        [HttpGet("Get")]
        [SwaggerResponse(200)]
        [SwaggerResponse(400, Type = typeof(string))]
        public async Task<IActionResult> ViewCommission(int commissionId)
        {
            if (false == int.TryParse(_jwtHelper.GetUserIdFromToken(HttpContext), out int userId))
            {
                return Unauthorized(new object());
            }
            Commission? commission = await _commissionService.GetUserCommission(userId, commissionId);
            if (commission != null)
            {
                UserInfo? creator = await _userInfoService.GetUserByCreatorId(commission.CreatorId);
                ViewCommissionResponse response = new()
                {
                    CommisionId = commission.CommissionId,
                    CreatedDate = commission.CreatedDate,
                    Deadline = commission.Deadline,
                    Price = commission.Price,
                    UserName = commission.UserInfo.NickName,
                    CommissionStatus = commission.CommissionStatus,
                    Description = commission.Description,
                    ArtistName = creator.NickName
                };
                return Ok(response);
            }
            return NotFound(new object());
        }

        [Authorize]
        [HttpPost("Creator/FinishCommission")]
        [SwaggerResponse(200)]
        [SwaggerResponse(400, Type = typeof(string))]
        public async Task<IActionResult> FinishCommission([FromForm] int commissionId, [FromForm]IFormFile image)
        {
            if (false == int.TryParse(_jwtHelper.GetCreatorIdFromToken(HttpContext),out int creatorId))
            {
                return Unauthorized(new { });
            }
            if (false == ImageType.IsSupportedImageType(image.ContentType))
            {
                return BadRequest(new
                {
                    Error = "Unsupported image type"
                });
            }
            var (isUpdated, message) = await _commissionService.FinishCommission(creatorId,commissionId,image);
            if (isUpdated)
            {
                return Ok(new { });
            }
            return BadRequest(new
            {
                Error = message
            });
        }
    }
}
