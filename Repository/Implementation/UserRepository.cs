using BusinessObject.NoSqlObject;
using BusinessObject.SqlObject;
using DataAccessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly IDaoFactory _daoFactory;
        private readonly IGenericDao<User> _userDao;
        public UserRepository(IDaoFactory daoFactory)
        {
            _daoFactory = daoFactory;
            _userDao = _daoFactory.CreateDao<User>();
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            IEnumerable<User>? users = await _userDao.GetByFilterAsync(u=>u.UserId.Equals(id));
            return users?.SingleOrDefault(); 
        }

        public async Task<IEnumerable<User>?> GetUsersByConditionAsync(Expression<Func<User, bool>> predicate)
        {
            IEnumerable<User>? users = await _userDao.GetByFilterAsync(predicate);
            return users;
        }
        public async Task AddUserAsync(User user)
        {
            await _userDao.AddAsync(user);
        }
    }
}
