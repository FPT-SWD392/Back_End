using BusinessObject.SqlObject;
using JwtTokenAuthorization;
using Microsoft.AspNetCore.Http;
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
        private readonly JwtTokenHelper _jwtHelper;
        public AuthenticationController(IAuthenticationService authenticationService, JwtTokenHelper jwtHelper)
        {
            _authenticationService = authenticationService;
            _jwtHelper = jwtHelper;
        }


        /// <summary>
        /// Authenticates a user by validating the provided credentials.
        /// </summary>
        /// <remarks>
        /// This method verifies the username and password provided by the user
        /// and returns a token if the authentication is successful.
        /// </remarks>
        /// <returns>
        ///  Status code 200, JWT token and user information if the authentication is successful; otherwise, returns status code 400.
        /// </returns>
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody]LoginRequest loginRequest)
        {
            User? user = await _authenticationService.Login(loginRequest.Email, loginRequest.Password);
            if (user == null)
            {
                return Unauthorized();
            }
            string token = _jwtHelper.GenerateToken(user);
            LoginResponse loginResponse = new()
            {
                Token = token,
                UserName = user.UserName,
                UserRole = user.Role,
                ProfilePictureUrl = user.ProfilePictureUrl
            };
            return Ok(loginResponse);
        }
        [HttpGet("Init")]
        public async Task<IActionResult> Create()
        {
            await _authenticationService.CreateUser();
            return Ok();
        }
    }
}
