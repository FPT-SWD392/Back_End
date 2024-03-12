using DataAccessObject.GenericQueryable;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace DataAccessObject
{
    public class SqlGenericDao<T> : IGenericDao<T> where T : class
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<T> _dbSet;
        public SqlGenericDao(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }
        public async Task CreateAsync(T item)
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
        public IGenericQueryable<T> Query()
        {
            return new SqlGenericQueryable<T>(_dbSet.AsQueryable());
        }
        
    }
}
