using BusinessObject.SqlObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface ICommissionRepository
    {
        public Task CreateNewCommission(Commission commission);
        public Task DeleteCommission(Commission commission);
        public Task UpdateCommission(Commission commission);
        public Task<List<Commission>> GetUserCommissions(int userId);
        public Task<List<Commission>> GetArtistCommissions(int creatorId);
        public Task<Commission?> GetCommission(int commissionId);
        public ISqlFluentRepository<Commission> Query();
    }
}
