using BusinessObject;
using BusinessObject.SqlObject;
using DataAccessObject;
using Repository.Interface;

namespace Repository.Implementation
{
    public class UserInfoRepository : IUserInfoRepository
    {
        private readonly IGenericDao<UserInfo> _userInfoDao;
        public UserInfoRepository(IDaoFactory daoFactory)
        {
            _userInfoDao = daoFactory.CreateDao<UserInfo>();
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
            return await _userInfoDao
                .Query()
                .ToListAsync();
        }

        public async Task<UserInfo?> GetUserById(int id)
        {
            return await _userInfoDao
                .Query()
                .Where(u=>u.UserId== id)
                .SingleOrDefaultAsync();
        }

        public async Task<UserInfo?> GetUserByEmail(string email)
        {
            return await _userInfoDao
                .Query()
                .Where(u => u.Email == email && u.Status == AccountStatus.Active)
                .SingleOrDefaultAsync();
        }
    }
}
