using BusinessObject.DTO;
using BusinessObject.SqlObject;
using System;

namespace Services
{
    public interface IUserInfoService
    {
        Task<UserInfo?> GetUserByUserId(int userId);
        Task<UserInfo?> GetUserByCreatorId(int creatorId);
        Task UpdateProfile(int id, string fullName, string location, string phoneNumber, string nickName);
        Task UpdateProfilePicture(int id, string imgBase64);
        Task<bool> VerifyOldPassword(int id, string oldPassword);
        Task UpdatePassword(int id, string newPassword);
        Task<RegisterResponse> Register(UserInfo userInfo);
        Task<UserInfo?> GetUserByUserEmail(string email);
        Task<UserInfo?> GetUserByUserPhone(string phoneNumber);
        Task<UserInfo?> GetUserByNickName(string nickName);
        Task AddAccountBalance(AddBalanceRequest addBalanceRequest ,int userId);
        Task<List<UserInfo>> GetAllUser();
    }
}
