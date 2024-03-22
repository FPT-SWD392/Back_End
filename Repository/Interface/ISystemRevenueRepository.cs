using BusinessObject.MongoDbObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface ISystemRevenueRepository
    {
        public Task CreateSystemRevenue(SystemRevenue imageInfo);
        public Task DeleteSystemRevenue(SystemRevenue imageInfo);
        public Task UpdateSystemRevenue(SystemRevenue imageInfo);
        public Task<SystemRevenue?> GetRevenueById(int id);
        public Task<List<SystemRevenue>> GetAllRevenues();
    }
}
