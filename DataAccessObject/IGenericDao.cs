using System.Linq.Expressions;
namespace DataAccessObject
{
    public interface IGenericDao<T> where T : class
    {

        public Task CreateAsync(T item);
        public Task DeleteAsync(T item);
        public Task UpdateAsync(T item);

        public IGenericDao<T> Where(Expression<Func<T, bool>> expression);
        public IGenericDao<T> Include(Expression<Func<T, object>> include);
        public IGenericDao<T> OrderBy(Expression<Func<T, object>> orderBy);
        public IGenericDao<T> OrderByDescending(Expression<Func<T, object>> column);
        public IGenericDao<T> Skip(int skip);
        public IGenericDao<T> Take(int take);
        public Task<List<T>> ToListAsync();
        public Task<long> CountAsync();
        public Task<T?> SingleOrDefaultAsync();
    }
}
