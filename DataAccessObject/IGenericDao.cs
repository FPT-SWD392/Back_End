using DataAccessObject.GenericQueryable;
using System.Linq.Expressions;
namespace DataAccessObject
{
    public interface IGenericDao<T> where T : class
    {

        public Task CreateAsync(T item);
        public Task DeleteAsync(T item);
        public Task UpdateAsync(T item);

        public IGenericQueryable<T> Query();
    }
}
