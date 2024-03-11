using BusinessObject.SqlObject;
using JwtTokenAuthorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using WebAPI.Model;

namespace WebAPI.Controllers
{
    /// <summary>
    /// This controller is used for guest to authorize, register or reset password.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ITokenHelper _jwtHelper;
        public AuthenticationController(IAuthenticationService authenticationService, ITokenHelper jwtHelper)
        {
            _authenticationService = authenticationService;
            _jwtHelper = jwtHelper;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody]LoginRequest loginRequest)
        {
            UserInfo? user = await _authenticationService.Login(loginRequest.Email, loginRequest.Password);
            if (user == null)
            {
                return Unauthorized();
            }
            string token = _jwtHelper.GenerateToken(user);
            LoginResponse loginResponse = new()
            {
                Token = token,
                UserName = user.FullName,
                ProfilePictureUrl = user.ProfilePicture
            };
            return Ok(loginResponse);
        }
    }
}
