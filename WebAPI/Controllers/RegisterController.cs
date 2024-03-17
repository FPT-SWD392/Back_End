using BusinessObject;
using BusinessObject.DTO;
using BusinessObject.SqlObject;
using JwtTokenAuthorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.IdentityModel.Tokens;
using Services;
using Swashbuckle.AspNetCore.Annotations;
using Utils.PasswordHasher;
using WebAPI.Model;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {

        private readonly IUserInfoService _userInfoService;
        private readonly IPasswordHasher _passwordHasher;
        public RegisterController(IUserInfoService userInfoService, IPasswordHasher passwordHasher)
        {
            _userInfoService = userInfoService;
            _passwordHasher = passwordHasher;
        }
        [HttpPost("Register")]
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
        //[Authorize]
        //[HttpGet("CheckEmail")]
        //public async Task<IActionResult> CheckEmail(string email)
        //{
        //    try
        //    {
        //        if (await _userInfoService.GetUserByUserEmail(email) != null)
        //        {
        //            return Ok();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //    return BadRequest();
        //}

        //[Authorize]
        //[HttpGet("CheckPhoneNumber")]
        //public async Task<IActionResult> CheckPhoneNumber(string phoneNumber)
        //{
        //    try
        //    {
        //        if (await _userInfoService.GetUserByUserPhone(phoneNumber) != null)
        //        {
        //            return Ok();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //    return BadRequest();
        //}

        //[Authorize]
        //[HttpGet("CheckNickName")]
        //public async Task<IActionResult> CheckNickName(string nickName)
        //{
        //    try
        //    {
        //        if (await _userInfoService.GetUserByNickName(nickName) != null)
        //        {
        //            return Ok();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //    return BadRequest();
        //}
    }
}
