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
        private readonly IReportService _reportInfo;

        public ReportController(ITokenHelper jwtHelper, IReportService reportInfo)
        {
            _jwtHelper = jwtHelper;
            _reportInfo = reportInfo;
        }
        // POST api/<ReportController>
        //for user view all of their reports
        [Authorize]
        [HttpGet("ViewReports")]
        public async Task<IActionResult> ViewReports()
        {
            int userId = Int32.Parse(_jwtHelper.GetUserIdFromToken(HttpContext));
            var report = await _reportInfo.GetAllReportsOfThatUser(userId);
            return Ok(report);
        }
        [Authorize]
        [HttpGet("GetAllReports")]
        public async Task<IActionResult> GetAllReports()
        {
            var report = await _reportInfo.GetAllReport();
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
                var check = await _reportInfo.ReportUser(report);
                if (check)
                {
                    return Ok();
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
                var check = await _reportInfo.ReportArtist(report);
                if (check)
                {
                    return Ok();
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
                var check = await _reportInfo.ReportArt(report);
                if (check)
                {
                    return Ok();
                }
                else return BadRequest("Can not report");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpPost("ReportPost")]
        public async Task<IActionResult> ReportPost([FromBody] ReportRequest report)
        {
            try
            {
                report.ReportedObjectType = BusinessObject.ReportedObjectType.Post;
                report.ReporterId = Int32.Parse(_jwtHelper.GetUserIdFromToken(HttpContext));
                var check = await _reportInfo.ReportPost(report);
                if (check)
                {
                    return Ok();
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
                var check = await _reportInfo.ReportCommission(report);
                if (check)
                {
                    return Ok();
                }
                else return BadRequest("Can not report");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
