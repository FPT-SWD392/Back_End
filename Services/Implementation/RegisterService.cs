using BusinessObject.SqlObject;
using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Interface;
using Utils.PasswordHasher;

namespace Services.Implementation
{
    public class RegisterService : IRegisterService
    {
        private readonly IUserInfoRepository _userRepository;
        public RegisterService(IUserInfoRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<string>?> Register(UserInfo userInfo, string password)
        {
            List<string> invalid = new List<string>();
            try
            {
                if (!password.Any(char.IsLetterOrDigit) || 
                    !password.Any(ch => !char.IsLetterOrDigit(ch)) || 
                    !(password.Length >= 6 && password.Length <= 100))
                {
                    invalid.Add("Invalid password. Password is invalided. The password have to be between 6 and 100 characters, this also required at least 1 special character.");
                }

                if (userInfo.PhoneNumber.Length != 10 || 
                    userInfo.PhoneNumber.Any(ch => !char.IsDigit(ch)))
                {
                    invalid.Add("Invalid phone number. The Telephone have to be 10 numbers");
                }
                else if(_userRepository
                        .Query()
                        .Where(x => x.PhoneNumber == userInfo.PhoneNumber)
                        .SingleOrDefaultAsync() != null)
                {
                    invalid.Add("This phone number has already been used.");
                }

                if (_userRepository
                        .Query()
                        .Where(x => x.Email == userInfo.Email)
                        .SingleOrDefaultAsync() != null)
                {
                    invalid.Add("This email has already been used.");
                }

                if (_userRepository
                        .Query()
                        .Where(x => x.NickName == userInfo.NickName)
                        .SingleOrDefaultAsync() != null)
                {
                    invalid.Add("This nickname has already been used.");
                }

                if (invalid.Count() == 0)
                {
                    await _userRepository.CreateNewUser(userInfo);
                    return null;
                }
                else
                {
                    return invalid;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
