using BusinessObject.SqlObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IUserRepository
    {
        public Task<User?> GetUserByIdAsync(int id);
        public Task<IEnumerable<User>?> GetUsersByConditionAsync(Expression<Func<User,bool>> predicate);
        public Task AddUserAsync(User user);

    }
}
