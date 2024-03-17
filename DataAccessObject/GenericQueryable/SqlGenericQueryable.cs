using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DataAccessObject.GenericQueryable
{
    internal class SqlGenericQueryable<T> : IGenericQueryable<T> where T : class
    {
        private IQueryable<T> _query;
        public SqlGenericQueryable(IQueryable<T> queryable)
        {
            _query = queryable;
        }
        public IGenericQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            _query = _query.Where(expression);
            return this;
        }
        public IGenericQueryable<T> Include(Expression<Func<T, object>> include)
        {
            _query = _query.Include(include);
            return this;
        }
        public IGenericQueryable<T> OrderBy(Expression<Func<T, object>> column)
        {
            _query = _query.OrderBy(column);
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
