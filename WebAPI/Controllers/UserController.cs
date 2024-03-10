using BusinessObject;
using BusinessObject.SqlObject;
using JwtTokenAuthorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Interface;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Utils.PasswordHasher;
using WebAPI.Model;
using Microsoft.AspNetCore.Authorization;
using Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ITokenHelper _jwtHelper;
        private readonly IUserInfoService _userInfo;
        private readonly IPasswordHasher _passwordHasher;
        public UserController(ITokenHelper jwtHelper,IPasswordHasher passwordHasher, IUserInfoService userInfo)
        {
            _jwtHelper = jwtHelper;
            _passwordHasher = passwordHasher;
            _userInfo = userInfo;
        }

        [Authorize]
        [HttpGet("GetAllInfoAboutUser")]
        public async Task<IActionResult> GetAllInfoAboutUser()
        {
            int userId = Int32.Parse(_jwtHelper.GetUserIdFromToken(HttpContext));
            var result = await _userInfo.GetUserByUserId(userId);
            return Ok(result);
        }

        [Authorize]
        [HttpPut("UserManageProfile")]
        public async Task<IActionResult> ManageProfile([FromQuery] UserProfile user)
        {
            try
            {
                int userId = Int32.Parse(_jwtHelper.GetUserIdFromToken(HttpContext));
                await _userInfo.UpdateProfile(userId, user.FullName, user.Location, user.PhoneNumber, user.NickName);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("UpdateProfilePicture")]
        public async Task<IActionResult> UpdateProfilePicture(string imgBase64)
        {
            try
            {
                int userId = Int32.Parse(_jwtHelper.GetUserIdFromToken(HttpContext));
                await _userInfo.UpdateProfilePicture(userId, imgBase64);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("UpdateProfilePassword")]
        public async Task<IActionResult> UpdateProfilePassword(string oldPassword, string newPassword)
        {
            try
            {
                int userId = Int32.Parse(_jwtHelper.GetUserIdFromToken(HttpContext));
                var check = await _userInfo.VerifyOldPassword(userId, oldPassword);
                if (check)
                {
                    await _userInfo.UpdatePassword(userId, newPassword);
                    return Ok();
                }
                else return BadRequest("Old password is not correct");
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
