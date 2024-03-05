
using BusinessObject.SqlObject;
using DataAccessObject;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementation
{
    public class UserInfoRepository : IUserInfoRepository
    {
        public readonly IGenericDao<UserInfo> _userInfoDao;
        private readonly ISqlFluentRepository<UserInfo> _sqlFluentRepository;
        public UserInfoRepository(
            IDaoFactory daoFactory, 
            ISqlFluentRepository<UserInfo> sqlFluentRepository)
        {
            _userInfoDao = daoFactory.CreateDao<UserInfo>();
            _sqlFluentRepository = sqlFluentRepository;
        }
        public async Task CreateNewUser(UserInfo userInfo)
        {
            await _userInfoDao.CreateAsync(userInfo);
        }

        public async Task DeleteUser(UserInfo userInfo)
        {
            await _userInfoDao.DeleteAsync(userInfo);
        }

        public async Task UpdateUser(UserInfo userInfo)
        {
            await _userInfoDao.UpdateAsync(userInfo);
        }

        public async Task<List<UserInfo>> GetAllUsers()
        {
            return await _userInfoDao.ToListAsync();
        }

        public async Task<UserInfo?> GetUserById(int id)
        {
            return await _userInfoDao
                .Where(u=>u.UserId== id)
                .SingleOrDefaultAsync();
        }

        public ISqlFluentRepository<UserInfo> Query()
        {
            return _sqlFluentRepository;
        }
    }
}
