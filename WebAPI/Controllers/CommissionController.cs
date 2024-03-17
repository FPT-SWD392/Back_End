using BusinessObject;
using BusinessObject.DTO;
using BusinessObject.SqlObject;
using JwtTokenAuthorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Services;
using Swashbuckle.AspNetCore.Annotations;
using WebAPI.Model;

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
        [SwaggerResponse(200)]
        [SwaggerResponse(400, "Deadline < Now error, or balance error", Type = typeof(string))]
        public async Task<IActionResult> CreateCommission([FromBody] CreateCommissionRequest commissionRequest)
        {
            try
            {
                if (commissionRequest.Deadline <= DateTime.Today)
                {
                    throw new Exception("Invalid deadline");
                }

                int userId = Int32.Parse(_jwtHelper.GetUserIdFromToken(HttpContext));
                await _commissionService.CreateCommission(commissionRequest.Deadline, commissionRequest.Price, commissionRequest.CreatorId, userId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("CancelCommission/{commissionId}")]
        [SwaggerResponse(200)]
        [SwaggerResponse(400, Type = typeof(string))]
        public async Task<IActionResult> CancelCommission(int commissionId)
        {
            try
            {
                int userId = Int32.Parse(_jwtHelper.GetUserIdFromToken(HttpContext));
                await _commissionService.UpdateCommissionStatus(commissionId, "cancel");
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("AcceptCommission/{commissionId}")]
        [SwaggerResponse(200)]
        [SwaggerResponse(400, Type = typeof(string))]
        public async Task<IActionResult> AcceptCommission(int commissionId)
        {
            try
            {
                int userId = Int32.Parse(_jwtHelper.GetUserIdFromToken(HttpContext));
                await _commissionService.UpdateCommissionStatus(commissionId, "accept");
                //await _commissionService.UpdateCommissionStatus(commissionId, CommissionStatus.Accepted);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("DenyCommission/{commissionId}")]
        [SwaggerResponse(200)]
        [SwaggerResponse(400, Type = typeof(string))]
        public async Task<IActionResult> DenyCommission(int commissionId)
        {
            try
            {
                int userId = Int32.Parse(_jwtHelper.GetUserIdFromToken(HttpContext));
                await _commissionService.UpdateCommissionStatus(commissionId, "deny");
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("ViewArtistAcceptedCommission")]
        [SwaggerResponse(200)]
        [SwaggerResponse(400, Type = typeof(string))]
        public async Task<IActionResult> ViewArtistAcceptedCommission()
        {
            int artistId = Int32.Parse(_jwtHelper.GetUserIdFromToken(HttpContext));
            List<Commission?> commission = await _commissionService.GetAcceptedCommissionByCreatorId(artistId);
            if (commission.Count > 0)
            {
                List<ViewCommissionResponse> response = new List<ViewCommissionResponse>();
                foreach (Commission item in commission)
                {
                    ViewCommissionResponse r = new ViewCommissionResponse
                    {
                        CommisionId = item.CommissionId,
                        CreatedDate = item.CreatedDate,
                        Deadline = item.Deadline,
                        Price = item.Price,
                        UserName = item.UserInfo.NickName,
                        CommissionStatus = item.CommissionStatus,
                        ArtistName = item.CreatorInfo.UserInfo.NickName,
                        ImageId = item.ImageId,
                        Rating = item.Rating,
                        Review = item.Review,
                    };
                    response.Add(r);
                }
                return Ok(response);
            }
            else
            {
                return BadRequest("Cannot find any commission.");
            }
        }

        [Authorize]
        [HttpGet("ViewCommission/{commissionId}")]
        [SwaggerResponse(200)]
        [SwaggerResponse(400, Type = typeof(string))]
        public async Task<IActionResult> ViewCommission(int commissionId)
        {
            Commission? commission = await _commissionService.GetCommissionByCommissionId(commissionId);

            if (commission != null)
            {
                ViewCommissionResponse response = new ViewCommissionResponse
                {
                    CommisionId = commission.CommissionId,
                    CreatedDate = commission.CreatedDate,
                    Deadline = commission.Deadline,
                    Price = commission.Price,
                    UserName = commission.UserInfo.NickName,
                    CommissionStatus = commission.CommissionStatus,
                    ArtistName = commission.CreatorInfo.UserInfo.NickName,
                    ImageId = commission.ImageId,
                    Rating = commission.Rating,
                    Review = commission.Review,
                };
                return Ok(response);
            }
            else
            {
                return BadRequest("Cannot find any commission.");
            }
        }

        [Authorize]
        [HttpPut("FinishCommission")]
        [SwaggerResponse(200)]
        [SwaggerResponse(400, Type = typeof(string))]
        public async Task<IActionResult> FinishCommission([FromBody] FinishCommissionRequest finishCommissionRequest)
        {
            try
            {
                await _commissionService.FinishCommission(finishCommissionRequest.CommisionId, finishCommissionRequest.ImageId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("ViewCommissionByCreatorId")]
        [SwaggerResponse(200)]
        [SwaggerResponse(400, Type = typeof(string))]
        public async Task<IActionResult> ViewCommissionByCreatorId()
        {
            int artistId = Int32.Parse(_jwtHelper.GetUserIdFromToken(HttpContext));
            List<Commission?> commission = await _commissionService.GetCommissionByCreatorId(artistId);
            if (commission.Count > 0)
            {
                List<ViewCommissionResponse> response = new List<ViewCommissionResponse>();
                foreach (Commission item in commission)
                {
                    ViewCommissionResponse r = new ViewCommissionResponse
                    {
                        CommisionId = item.CommissionId,
                        CreatedDate = item.CreatedDate,
                        Deadline = item.Deadline,
                        Price = item.Price,
                        UserName = item.UserInfo.NickName,
                        CommissionStatus = item.CommissionStatus,
                        ArtistName = item.CreatorInfo.UserInfo.NickName,
                        ImageId = item.ImageId,
                        Rating = item.Rating,
                        Review = item.Review,
                    };
                    response.Add(r);
                }
                return Ok(response);
            }
            else
            {
                return BadRequest("Cannot find any commission.");
            }
        }

        [Authorize]
        [HttpGet("ViewCommissionByUserId")]
        [SwaggerResponse(200)]
        [SwaggerResponse(400, Type = typeof(string))]
        public async Task<IActionResult> ViewCommissionByUserId()
        {
            int userId = Int32.Parse(_jwtHelper.GetUserIdFromToken(HttpContext));
            List<Commission?> commission = await _commissionService.GetCommissionByUserId(userId);
            if (commission.Count > 0)
            {
                List<ViewCommissionResponse> response = new List<ViewCommissionResponse>();
                foreach (Commission item in commission)
                {
                    ViewCommissionResponse r = new ViewCommissionResponse
                    {
                        CommisionId = item.CommissionId,
                        CreatedDate = item.CreatedDate,
                        Deadline = item.Deadline,
                        Price = item.Price,
                        UserName = item.UserInfo.NickName,
                        CommissionStatus = item.CommissionStatus,
                        ArtistName = item.CreatorInfo.UserInfo.NickName,
                        ImageId = item.ImageId,
                        Rating = item.Rating,
                        Review = item.Review,
                    };
                    response.Add(r);
                }
                return Ok(response);
            }
            else
            {
                return BadRequest("Cannot find any commission.");
            }
        }
    }
}
