using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface ISqlFluentRepository<T> where T : class
    {
        public ISqlFluentRepository<T> Where(Expression<Func<T, bool>> expression);
        public ISqlFluentRepository<T> Include(Expression<Func<T, object>> include);
        public ISqlFluentRepository<T> OrderBy(Expression<Func<T, object>> orderBy);
        public ISqlFluentRepository<T> OrderByDescending(Expression<Func<T, object>> column);
        public ISqlFluentRepository<T> Skip(int skip);
        public ISqlFluentRepository<T> Take(int take);
        public Task<List<T>> ToListAsync();
        public Task<long> CountAsync();
        public Task<T?> SingleOrDefaultAsync();
    }
}
