using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObject
{
    public interface IGenericDao<T> where T : class
    {
        public Task<IEnumerable<T>?> GetAllAsync();
        public Task<long> CountAllAsync();
        public Task<IEnumerable<T>?> GetByFilterAsync(Expression<Func<T, bool>> predicate);
        public Task<long> CountByFilterAsync(Expression<Func<T, bool>> predicate);
        public Task<IEnumerable<T>?> GetByFilterPagedAsync(
            int pageNumber,
            int pageSize,
            bool isAscending,
            Expression<Func<T, object>> propertySelector,
            Expression<Func<T, bool>> predicate);
        public Task AddAsync(T item);
        public Task DeleteAsync(T item);
        public Task UpdateAsync(T item);
    }
}
