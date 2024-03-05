using BusinessObject.SqlObject;
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
        public Task<List<UserInfo>> GetAllUsers();
        public ISqlFluentRepository<UserInfo> Query();
    }
}
