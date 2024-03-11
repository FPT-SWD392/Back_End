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
    }
}