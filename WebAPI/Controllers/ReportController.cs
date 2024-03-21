using BusinessObject.DTO;
using JwtTokenAuthorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Interface;
using Services;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly ITokenHelper _jwtHelper;
        private readonly IReportService _reportService;
        private readonly IUserInfoService _userService;
        private readonly IArtService _artService;
        public ReportController(ITokenHelper jwtHelper, IReportService reportService, IUserInfoService userService, IArtService artService)
        {
            _jwtHelper = jwtHelper;
            _reportService = reportService;
            _userService = userService;
            _artService = artService;
        }
        // POST api/<ReportController>
        //for user view all of their reports
        [Authorize]
        [HttpGet("ViewReports")]
        public async Task<IActionResult> ViewReports()
        {
            int userId = Int32.Parse(_jwtHelper.GetUserIdFromToken(HttpContext));
            var report = await _reportService.GetAllReportsOfThatUser(userId);
            return Ok(report);
        }
        [Authorize]
        [HttpGet("GetAllReports")]
        public async Task<IActionResult> GetAllReports()
        {
            var report = await _reportService.GetAllReport();
            return Ok(report);
        }
        // POST api/<ReportController>
        [Authorize]
        [HttpPost("ReportUser")]
        public async Task<IActionResult> ReportUser([FromBody]ReportRequest report)
        {
            try
            {
                report.ReportedObjectType = BusinessObject.ReportedObjectType.User;
                report.ReporterId = Int32.Parse(_jwtHelper.GetUserIdFromToken(HttpContext));
                var check = await _reportService.ReportUser(report);
                if (check)
                {
                    return Ok(new {});
                }
                else return BadRequest("Can not report");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost("ReportArtist")]
        public async Task<IActionResult> ReportArtist([FromBody] ReportRequest report)
        {
            try
            {
                report.ReportedObjectType = BusinessObject.ReportedObjectType.Artist;
                report.ReporterId = Int32.Parse(_jwtHelper.GetUserIdFromToken(HttpContext));
                var check = await _reportService.ReportArtist(report);
                if (check)
                {
                    return Ok(new {});
                }
                else return BadRequest("Can not report");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpPost("ReportArt")]
        public async Task<IActionResult> ReportArt([FromBody] ReportRequest report)
        {
            try
            {
                report.ReportedObjectType = BusinessObject.ReportedObjectType.Art;
                report.ReporterId = Int32.Parse(_jwtHelper.GetUserIdFromToken(HttpContext));
                var check = await _reportService.ReportArt(report);
                if (check)
                {
                    return Ok(new {});
                }
                else return BadRequest("Can not report");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpPost("ReportCommission")]
        public async Task<IActionResult> ReportCommission([FromBody] ReportRequest report)
        {
            try
            {
                report.ReportedObjectType = BusinessObject.ReportedObjectType.Commission;
                report.ReporterId = Int32.Parse(_jwtHelper.GetUserIdFromToken(HttpContext));
                var check = await _reportService.ReportCommission(report);
                if (check)
                {
                    return Ok(new {});
                }
                else return BadRequest("Can not report");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpPost("BanUser/{id}")]
        public async Task<IActionResult> BanUser(int id)
        {
            try
            {
                var ban = await _userService.BanUser(id);
                if (ban)
                {
                    return Ok(new {});
                }
                else return BadRequest("Unsuccessful. Try again");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost("BanArt/{id}")]
        public async Task<IActionResult> BanArt(int id)
        {
            try
            {
                var ban = await _artService.BanArt(id);
                if (ban)
                {
                    return Ok(new {});
                }
                else return BadRequest("Unsuccessful. Try again");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
