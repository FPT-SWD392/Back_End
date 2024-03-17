using Amazon.Auth.AccessControlPolicy.ActionIdentifiers;
using BusinessObject;
using BusinessObject.DTO;
using BusinessObject.SqlObject;
using Microsoft.Identity.Client;
using Repository.Implementation;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Utils.PasswordHasher;
using WebAPI.Model;
using static System.Net.Mime.MediaTypeNames;

namespace Services.Implementation
{
    public class UserInfoService : IUserInfoService
    {
        private readonly IUserInfoRepository _userRepository;
        private readonly ICreatorInfoRepository _creatorInfoRepository;
        private readonly IPasswordHasher _passwordHasher;
        public UserInfoService(IUserInfoRepository userInfoRepository, ICreatorInfoRepository creatorInfoRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userInfoRepository;
            _creatorInfoRepository = creatorInfoRepository;
            _passwordHasher = passwordHasher;
        }

        public  Task<UserInfo?> GetUserByUserId(int userId)
        {
            return _userRepository.GetUserById(userId);
        }

        public async Task<UserInfo?> GetUserByCreatorId(int creatorId)
        {
            CreatorInfo creator = await _creatorInfoRepository.GetCreatorInfo(creatorId);
            return creator.UserInfo;
        }
        public async Task UpdateProfile(int id, string fullName, string location, string phoneNumber, string nickName)
        {
            try
            {
                var user = await _userRepository.GetUserById(id);
                if (user != null)
                {
                    user.FullName = fullName;
                    user.Location = location;
                    user.PhoneNumber = phoneNumber;
                    user.NickName = nickName;
                    await _userRepository.UpdateUser(user);
                }
                else
                {
                    throw new Exception("Cannot find user");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task UpdateProfilePicture(int id, string imgBase64)
        {
            try
            {
                var user = await _userRepository.GetUserById(id);
                if (user != null)
                {
                    user.ProfilePicture = imgBase64;
                    await _userRepository.UpdateUser(user);
                }
                else
                {
                    throw new Exception("Cannot find user");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> VerifyOldPassword(int id, string oldPassword)
        {
            try
            {
                var user = await _userRepository.GetUserById(id);
                if (user != null)
                {
                    if (_passwordHasher.VerifyPassword(oldPassword, user.PasswordHash))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    throw new Exception("Cannot find user");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task UpdatePassword(int id, string newPassword)
        {
            try
            {
                var user = await _userRepository.GetUserById(id);
                if (user != null)
                {
                    user.PasswordHash = _passwordHasher.HashPassword(newPassword);
                    await _userRepository.UpdateUser(user);
                }
                else
                {
                    throw new Exception("Cannot find user");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<UserInfo?> GetUserByNickName(string nickName)
        {
            return await _userRepository.GetUserByNickName(nickName);
        }

        public async Task<UserInfo?> GetUserByUserEmail(string email)
        {
            return await _userRepository.GetUserByEmail(email);
        }

        public async Task<UserInfo?> GetUserByUserPhone(string phoneNumber)
        {
            return await _userRepository.GetUserByPhoneNumber(phoneNumber);
        }

        public async Task<RegisterResponse> Register(UserInfo userInfo)
        {
            try
            {
                bool errorCheck = false;
                var registerReponse = new RegisterResponse() {
                    IsSuccess = false
                };
                if (await this.GetUserByUserEmail(userInfo.Email) != null)
                {
                    errorCheck = true;
                    registerReponse.ErrorEmail = "This email has been used by another user";
                }
                if (await this.GetUserByUserPhone(userInfo.PhoneNumber) != null)
                {
                    errorCheck = true;
                    registerReponse.ErrorPhoneNumber = "This phone number has been used by another user";
                }
                if (await this.GetUserByNickName(userInfo.NickName) != null)
                {
                    errorCheck = true;
                    registerReponse.ErrorNickName = "This nickname has been used by another user";
                }
                if (errorCheck)
                {
                    return registerReponse;
                }
                registerReponse.IsSuccess = true;
                await _userRepository.CreateNewUser(userInfo);
                return registerReponse;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task AddAccountBalance(double amount, int userId)
        {
            try
            {
                var userInfo = await this.GetUserByUserId(userId);
                if (userInfo != null)
                {
                    userInfo.Balance += amount;
                    await _userRepository.UpdateUser(userInfo);
                }
            } 
            catch (Exception ex)
            {
                throw new Exception("Unhandled Error");
            }
        }
    }
}
