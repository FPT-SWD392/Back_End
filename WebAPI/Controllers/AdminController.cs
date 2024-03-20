using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        [HttpPost("Login")]
        [SwaggerResponse(200)]
        [SwaggerResponse(401)]
        public async Task<IActionResult> Login([Required]string email, [Required]string password)
        {
            return Ok(await _adminService.Login(email, password));
        }
        [HttpPost("CreateNewAdminAccount")]
        [Authorize(Roles = "SYSADMIN")]
        public async Task<IActionResult> CreateNewAdmin()
        {
            return Ok();
        }
    }
}
