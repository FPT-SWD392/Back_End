using DataAccessObject;
using System.Linq.Expressions;

namespace Repository
{
    public class MongoFluentRepository<T> : IMongoFluentRepository<T> where T : class
    {
        private readonly IGenericDao<T> _dao;
        public MongoFluentRepository(IDaoFactory daoFactory)
        {
            _dao = daoFactory.CreateDao<T>();
        }
        public async Task<long> CountAsync()
        {
            return await _dao.CountAsync();
        }

        public IMongoFluentRepository<T> OrderBy(Expression<Func<T, object>> orderBy)
        {
            _dao.OrderBy(orderBy);
            return this;
        }

        public IMongoFluentRepository<T> OrderByDescending(Expression<Func<T, object>> column)
        {
            _dao.OrderByDescending(column);
            return this;
        }

        public async Task<T?> SingleOrDefaultAsync()
        {
            return await _dao.SingleOrDefaultAsync();
        }

        public IMongoFluentRepository<T> Skip(int skip)
        {
            _dao.Skip(skip);
            return this;
        }

        public IMongoFluentRepository<T> Take(int take)
        {
            _dao.Take(take);
            return this;
        }

        public async Task<List<T>> ToListAsync()
        {
            return await _dao.ToListAsync();
        }

        public IMongoFluentRepository<T> Where(Expression<Func<T, bool>> expression)
        {
            _dao.Where(expression);
            return this;
        }
    }
}
