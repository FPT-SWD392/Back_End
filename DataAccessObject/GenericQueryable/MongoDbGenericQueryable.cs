using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq.Expressions;

namespace DataAccessObject.GenericQueryable
{
    public class MongoDbGenericQueryable<T> : IGenericQueryable<T> where T : class
    {
        private IMongoQueryable<T> _query;
        public MongoDbGenericQueryable(IMongoQueryable<T> query)
        {
            _query = query;
        }
        public async Task<T?> SingleOrDefaultAsync()
        {
            return await _query.SingleOrDefaultAsync();
        }
        public async Task<long> CountAsync()
        {
            return await _query.CountAsync();
        }

        public IGenericQueryable<T> Include(Expression<Func<T, object>> include)
        {
            throw new NotSupportedException("Include is not supported in MongoDB");
        }

        public IGenericQueryable<T> OrderBy(Expression<Func<T, object>> orderBy)
        {
            _query = _query.OrderBy(orderBy);
            return this;
        }

        public IGenericQueryable<T> OrderByDescending(Expression<Func<T, object>> column)
        {
            _query = _query.OrderByDescending(column);
            return this;
        }

        public IGenericQueryable<T> Skip(int skip)
        {
            _query = _query.Skip(skip);
            return this;
        }

        public IGenericQueryable<T> Take(int take)
        {
            _query = _query.Take(take);
            return this;
        }

        public async Task<List<T>> ToListAsync()
        {
            return await _query.ToListAsync();
        }

        public IGenericQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            _query = _query.Where(expression);
            return this;
        }
    }
}
