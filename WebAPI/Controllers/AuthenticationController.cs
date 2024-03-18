using BusinessObject;
using BusinessObject.DTO;
using BusinessObject.SqlObject;
using JwtTokenAuthorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Implementation;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using Utils.PasswordHasher;
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
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserInfoService _userInfoService;
        public AuthenticationController(IAuthenticationService authenticationService, ITokenHelper jwtHelper, IPasswordHasher passwordHasher, IUserInfoService userInfoService)
        {
            _authenticationService = authenticationService;
            _jwtHelper = jwtHelper;
            _passwordHasher = passwordHasher;
            _userInfoService = userInfoService;
        }
        [HttpPost("Login")]
        [SwaggerResponse(200, Type = typeof(LoginResponse))]
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
        [HttpPut("Register")]
        [AllowAnonymous]
        [SwaggerResponse(200)]
        [SwaggerResponse(400, Type = typeof(RegisterResponse))]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            try
            {
                UserInfo userInfo = new UserInfo
                {
                    Email = registerRequest.Email,
                    PasswordHash = _passwordHasher.HashPassword(registerRequest.Password),
                    Status = AccountStatus.Active,
                    FullName = registerRequest.FullName,
                    Location = registerRequest.Location,
                    PhoneNumber = registerRequest.PhoneNumber,
                    NickName = registerRequest.NickName,
                    JoinDate = DateTime.Today,
                    Balance = 0
                };

                RegisterResponse registerResponse = await _userInfoService.Register(userInfo);
                if (registerResponse.IsSuccess)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(registerResponse);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
