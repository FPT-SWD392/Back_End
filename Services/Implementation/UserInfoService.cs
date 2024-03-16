using Amazon.Auth.AccessControlPolicy.ActionIdentifiers;
using BusinessObject;
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

        public async Task Register(UserInfo userInfo)
        {
            try
            {
                await _userRepository.CreateNewUser(userInfo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
