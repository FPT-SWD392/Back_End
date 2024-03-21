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
        private readonly ITransactionHistoryRepository _transactionHistoryRepository;
        private readonly IPasswordHasher _passwordHasher;
        public UserInfoService(IUserInfoRepository userInfoRepository, ICreatorInfoRepository creatorInfoRepository, IPasswordHasher passwordHasher, ITransactionHistoryRepository transactionHistoryRepository)
        {
            _userRepository = userInfoRepository;
            _creatorInfoRepository = creatorInfoRepository;
            _passwordHasher = passwordHasher;
            _transactionHistoryRepository = transactionHistoryRepository;
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
        public async Task<UserProfile?> GetUserInfo (int userid)
        {
            var user = await _userRepository.GetUserById(userid);
            if (user != null)
            {
                var profile = new UserProfile()
                {
                    NickName = user.NickName,
                    FullName = user.FullName,
                    Location = user.Location,
                    PhoneNumber = user.PhoneNumber,
                    UserId = userid,
                    Email = user.Email,
                    Balance = user.Balance
                };
                if (user.CreatorId != null)
                {
                    var creator = await _creatorInfoRepository.GetCreatorInfo(user.CreatorId.Value);
                    profile.ContactInfo = creator.ContactInfo;
                    profile.Bio = creator.Bio;
                }
                return profile;
            }
            else return null;
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
        public async Task<bool> BanUser(int id)
        {
            try
            {
                var user = await _userRepository.GetUserById(id);
                if (user != null)
                {
                    user.Status = AccountStatus.Banned;
                    await _userRepository.UpdateUser(user);
                    return true;
                }
                else
                {
                    return false;
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

        public async Task AddAccountBalance(AddBalanceRequest addBalanceRequest, int userId)
        {
            try
            {
                var userInfo = await this.GetUserByUserId(userId);
                if (addBalanceRequest.IsSuccess)
                {
                    userInfo.Balance += addBalanceRequest.Amount;
                    await _userRepository.UpdateUser(userInfo);
                    string note = "";
                    if (addBalanceRequest.TransactionType == TransactionType.DepositManualAdmin)
                    {
                        note = "Admin added to your wallet " + addBalanceRequest.Amount;
                    }
                    else
                    {
                        note = "You added to your wallet " + addBalanceRequest.Amount;
                    }
                    var transactionHistory = new TransactionHistory()
                    {
                        UserId = userId,
                        Note = note,
                        TransactionType = addBalanceRequest.TransactionType,
                        Amount = addBalanceRequest.Amount,
                        TransactionDate = DateTime.UtcNow,
                        IsSuccess = true,
                    };
                    await _transactionHistoryRepository.CreateTransactionHistory(transactionHistory);
                }
                else
                {
                    string note = "Transaction FAILED: " + addBalanceRequest.Amount;
                    var transactionHistory = new TransactionHistory()
                    {
                        UserId = userId,
                        Note = note,
                        TransactionType = addBalanceRequest.TransactionType,
                        TransactionDate = DateTime.UtcNow,
                        Amount = addBalanceRequest.Amount,
                        IsSuccess = false,
                    };
                    await _transactionHistoryRepository.CreateTransactionHistory(transactionHistory);
                }
                

            } 
            catch (Exception ex)
            {
                throw new Exception("Unhandled Error");
            }
        }

        public async Task<List<UserInfo>> GetAllUser()
        {
            return await _userRepository.GetAllUsers();
        }
    }
}
