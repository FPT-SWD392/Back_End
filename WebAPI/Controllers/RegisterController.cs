using BusinessObject;
using BusinessObject.SqlObject;
using JwtTokenAuthorization;
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

        private readonly IRegisterService _registerServices;
        private readonly IPasswordHasher _passwordHasher;
        public RegisterController(IRegisterService registerServices, IPasswordHasher passwordHasher)
        {
            _registerServices = registerServices;
            _passwordHasher = passwordHasher;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
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

            List<string> result = await _registerServices.Register(userInfo, registerRequest.Password);
            if (result.Count() == 0)
            {
                return Ok();
            }
            else
            {
                RegisterResponse registerResponse = new RegisterResponse();
                foreach (string item in result)
                {
                    if(item.ToLower().Contains("password")) registerResponse.PasswordInvalid = item;
                    else if(item.ToLower().Contains("email")) registerResponse.EmailInvalid = item;
                    else if(item.ToLower().Contains("phone")) registerResponse.PhoneNumberInvalid = item;
                    else if(item.ToLower().Contains("nickname")) registerResponse.NickNameInvalid = item;
                }
                return BadRequest(registerResponse);
            }
        }
    }
}
