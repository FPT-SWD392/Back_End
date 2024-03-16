using BusinessObject;
using BusinessObject.SqlObject;
using JwtTokenAuthorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Services;
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
        [Authorize]
        [HttpPost("Register")]
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

                await _userInfoService.Register(userInfo);
                //return Created("OK", null);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return BadRequest();
        }

        [Authorize]
        [HttpGet("CheckEmail")]
        public async Task<IActionResult> CheckEmail(string email)
        {
            try
            {
                if (await _userInfoService.GetUserByUserEmail(email) != null)
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return BadRequest();
        }

        [Authorize]
        [HttpGet("CheckPhoneNumber")]
        public async Task<IActionResult> CheckPhoneNumber(string phoneNumber)
        {
            try
            {
                if (await _userInfoService.GetUserByUserPhone(phoneNumber) != null)
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return BadRequest();
        }

        [Authorize]
        [HttpGet("CheckNickName")]
        public async Task<IActionResult> CheckNickName(string nickName)
        {
            try
            {
                if (await _userInfoService.GetUserByNickName(nickName) != null)
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return BadRequest();
        }
    }
}
