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
using BusinessObject.DTO;
using Swashbuckle.AspNetCore.Annotations;

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
            var result = await _userInfo.GetUserInfo(userId);
            return Ok(result);
        }
        [Authorize]
        [HttpGet("GetAllUser")]
        public async Task<IActionResult> GetAllUser()
        {
            var result = await _userInfo.GetAllUser();
            return Ok(result);
        }
        

        [HttpPut("GetAnotherInfo/{id}")]
        public async Task<IActionResult> GetAnotherInfo(int id)
        {
            try
            {
                var result = await _userInfo.GetUserInfo(id);
                if (result != null)
                {
                    return Ok(result);
                }
                else return BadRequest("User not found");
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
                    return Ok(new object
                    {

                    });
                }
                else return BadRequest("Old password is not correct");
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpPost("AddAccountBalanceUser")]
        [SwaggerResponse(200)]
        [SwaggerResponse(400)]
        public async Task<IActionResult> AddAccountBalanceUser([FromBody] AddBalanceRequest addBalanceRequest)
        {
            var userId = Int32.Parse(_jwtHelper.GetUserIdFromToken(HttpContext));
            if (addBalanceRequest.UserId == null || userId == addBalanceRequest.UserId)
            {
                
                if (addBalanceRequest.TransactionType != TransactionType.DepositVnPay 
                    && addBalanceRequest.TransactionType != TransactionType.DepositMomo
                    && addBalanceRequest.TransactionType != TransactionType.DepositOther)
                {
                    return BadRequest("Wrong transaction type for this api call (accept value range 0-2: 0 - vnpay, 1 - momo, 2 - other)");
                }
                try
                {
                    await _userInfo.AddAccountBalance(addBalanceRequest, userId);
                    return Ok(new object
                    {

                    });
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return BadRequest("UserId must be null or equal to logged in user in this API call");
            }
            

            
        }
        [Authorize]
        [HttpPost("AddAccountBalanceAdmin")]
        [SwaggerResponse(200)]
        [SwaggerResponse(400)]
        public async Task<IActionResult> AddAccountBalanceAdmin([FromBody] AddBalanceRequest addBalanceRequest)
        {
            try
            {
                if (addBalanceRequest.UserId != null) 
                {
                    if (addBalanceRequest.TransactionType != TransactionType.DepositManualAdmin)
                    {
                        return BadRequest("Wrong transaction type for this api call (accept value: 3)");
                    }
                    await _userInfo.AddAccountBalance(addBalanceRequest, (int) addBalanceRequest.UserId);
                    return Ok(new object
                    {

                    });
                }
                else
                {
                    return BadRequest("UserId must exist in this API call");
                }
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


    }
}
