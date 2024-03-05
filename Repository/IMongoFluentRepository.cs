using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IMongoFluentRepository<T> where T : class
    {
        public IMongoFluentRepository<T> Where(Expression<Func<T, bool>> expression);
        public IMongoFluentRepository<T> OrderBy(Expression<Func<T, object>> orderBy);
        public IMongoFluentRepository<T> OrderByDescending(Expression<Func<T, object>> column);
        public IMongoFluentRepository<T> Skip(int skip);
        public IMongoFluentRepository<T> Take(int take);
        public Task<List<T>> ToListAsync();
        public Task<long> CountAsync();
        public Task<T?> SingleOrDefaultAsync();
    }
}
