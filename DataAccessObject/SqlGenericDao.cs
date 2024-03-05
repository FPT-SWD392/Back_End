using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace DataAccessObject
{
    public class SqlGenericDao<T> : IGenericDao<T> where T : class
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<T> _dbSet;
        private IQueryable<T> _query;
        public SqlGenericDao(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
            _query = _dbSet.AsQueryable();
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

        public IGenericDao<T> Where(Expression<Func<T, bool>> expression)
        {
            _query = _dbSet.Where(expression);
            return this;
        }
        public IGenericDao<T> Include(Expression<Func<T, object>> include)
        {
            _query = _query.Include(include);
            return this;
        }
        public IGenericDao<T> OrderBy(Expression<Func<T, object>> column)
        {
            _query = _query.OrderBy(column);
            return this;
        }
        public IGenericDao<T> OrderByDescending(Expression<Func<T, object>> column)
        {
            _query = _query.OrderByDescending(column);
            return this;
        }
        public IGenericDao<T> Skip(int skip)
        {
            _query = _query.Skip(skip);
            return this;
        }
        public IGenericDao<T> Take(int take)
        {
            _query = _query.Take(take);
            return this;
        }
        public async Task<List<T>> ToListAsync()
        {
            return await _query.ToListAsync();
        }
        public async Task<long> CountAsync()
        {
            return await _query.CountAsync();
        }

        public Task<T?> SingleOrDefaultAsync()
        {
            return _query.SingleOrDefaultAsync();
        }
    }
}
