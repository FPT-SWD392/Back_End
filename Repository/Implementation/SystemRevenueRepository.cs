using BusinessObject.MongoDbObject;
using DataAccessObject;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementation
{
    public class SystemRevenueRepository : ISystemRevenueRepository
    {
        private readonly IGenericDao<SystemRevenue> _dao;
        public SystemRevenueRepository(IDaoFactory daoFactory)
        {
            _dao = daoFactory.CreateDao<SystemRevenue>();
        }
        public async Task CreateSystemRevenue(SystemRevenue systemRevenue)
        {
            await _dao.CreateAsync(systemRevenue);
        }

        public async Task DeleteSystemRevenue(SystemRevenue systemRevenue)
        {
            await _dao.DeleteAsync(systemRevenue);
        }

        public async Task<List<SystemRevenue>> GetAllRevenues()
        {
            return await _dao.Query().ToListAsync();
        }

        public async Task<List<SystemRevenue>> GetAllRevenuesInOneMonth()
        {
            return await _dao.Query().Where(m => m.Date >= DateTime.Now.AddDays(-30)).ToListAsync();
        }



        /// <summary>
        /// DO NOT USE, NOT IMPLEMENTED YET
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<SystemRevenue?> GetRevenueById(int id)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// DO NOT USE, NOT IMPLEMENTED YET
        /// </summary>
        /// <param name="systemRevenue"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task UpdateSystemRevenue(SystemRevenue systemRevenue)
        {
            throw new NotImplementedException();
        }
    }
}
