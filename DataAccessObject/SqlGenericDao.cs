using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccessObject
{
    internal class SqlGenericDao<T> : IGenericDao<T> where T : class
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<T> _dbSet;
        public SqlGenericDao(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }
        public async Task<IEnumerable<T>?> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }
        public async Task<long> CountAllAsync()
        {
            return await _dbSet.CountAsync();
        }
        public async Task<IEnumerable<T>?> GetByFilterAsync(Expression<Func<T,bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }
        public async Task<long> CountByFilterAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).CountAsync();
        }
        public async Task<IEnumerable<T>?> GetByFilterPagedAsync(
            int pageNumber, 
            int pageSize, 
            bool isAscending, 
            Expression<Func<T,object>> propertySelector,
            Expression<Func<T, bool>> predicate)
        {
            if (pageNumber < 1) throw new Exception("Invalid page number");
            if (pageSize < 1 ) throw new Exception("Invalid page size");
            int firstPageElementNumber = (pageNumber - 1) * pageSize;
            if (isAscending)
            {
                return await _dbSet
                    .Where(predicate)
                    .OrderBy(propertySelector)
                    .Skip(firstPageElementNumber)
                    .Take(pageSize)
                    .ToListAsync();
            }
            return await _dbSet
                .Where(predicate)
                .OrderByDescending(propertySelector)
                .Skip(firstPageElementNumber)
                .Take(pageSize)
                .ToListAsync();
        }
        public async Task AddAsync(T item)
        {
            _dbSet.Add(item);
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(T item)
        {
            _dbSet.Remove(item);
            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(T item)
        {
            _dbSet.Attach(item).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
