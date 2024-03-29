﻿using BusinessObject.SqlObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IUserInfoRepository
    {
        public Task CreateNewUser(UserInfo userInfo);
        public Task DeleteUser(UserInfo userInfo);
        public Task UpdateUser(UserInfo userInfo);
        public Task<UserInfo?> GetUserById(int id);
        public Task<UserInfo?> GetUserByEmail(string email);
        public Task<List<UserInfo>> GetAllUsers();
        public Task<UserInfo?> GetUserByPhoneNumber(string phoneNumber);
        public Task<UserInfo?> GetUserByNickName(string nickname);
        public Task<UserInfo?> GetUserByCreatorId(int creatorId);
    }
}
