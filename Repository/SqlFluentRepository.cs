using DataAccessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class SqlFluentRepository<T> : ISqlFluentRepository<T> where T : class
    {
        IGenericDao<T> _dao;
        public SqlFluentRepository(IDaoFactory daoFactory)
        {
            _dao = daoFactory.CreateDao<T>();
        }
        public async Task<long> CountAsync()
        {
            return await _dao.CountAsync();
        }

        public ISqlFluentRepository<T> Include(Expression<Func<T, object>> include)
        {
            _dao.Include(include);
            return this;
        }

        public ISqlFluentRepository<T> OrderBy(Expression<Func<T, object>> orderBy)
        {
            _dao.OrderBy(orderBy); 
            return this;
        }

        public ISqlFluentRepository<T> OrderByDescending(Expression<Func<T, object>> column)
        {
            _dao.OrderByDescending(column);
            return this;
        }

        public async Task<T?> SingleOrDefaultAsync()
        {
            return await _dao.SingleOrDefaultAsync();
        }

        public ISqlFluentRepository<T> Skip(int skip)
        {
            _dao.Skip(skip);
            return this;
        }

        public ISqlFluentRepository<T> Take(int take)
        {
            _dao.Take(take);
            return this;
        }

        public async Task<List<T>> ToListAsync()
        {
            return await _dao.ToListAsync();
        }

        public ISqlFluentRepository<T> Where(Expression<Func<T, bool>> expression)
        {
            _dao.Where(expression);
            return this;
        }
    }
}
