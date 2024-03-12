using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObject.GenericQueryable
{
    public interface IGenericQueryable <T> where T : class
    {
        public IGenericQueryable<T> Where(Expression<Func<T, bool>> expression);
        public IGenericQueryable<T> Include(Expression<Func<T, object>> include);
        public IGenericQueryable<T> OrderBy(Expression<Func<T, object>> orderBy);
        public IGenericQueryable<T> OrderByDescending(Expression<Func<T, object>> column);
        public IGenericQueryable<T> Skip(int skip);
        public IGenericQueryable<T> Take(int take);
        public Task<List<T>> ToListAsync();
        public Task<long> CountAsync();
        public Task<T?> SingleOrDefaultAsync();
    }
}
