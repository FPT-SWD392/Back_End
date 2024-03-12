using BusinessObject.SqlObject;
using DataAccessObject;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementation
{
    public class CommissionRepository : ICommissionRepository
    {
        private readonly IGenericDao<Commission> _dao;
        public CommissionRepository(IDaoFactory daoFactory)
        {
            _dao = daoFactory.CreateDao<Commission>();
        }
        public async Task CreateNewCommission(Commission commission)
        {
            await _dao.CreateAsync(commission);
        }

        public async Task DeleteCommission(Commission commission)
        {
            await _dao.DeleteAsync(commission);
        }

        public async Task<List<Commission>> GetAcceptedCommissionByCreatorId(int creatorId)
        {
            return await _dao
                .Where(x => x.CreatorId == creatorId && x.CommissionStatus == BusinessObject.CommissionStatus.Accepted)
                .Include(x => x.UserInfo)
                .ToListAsync();
        }

        public async Task<List<Commission>> GetArtistCommissions(int creatorId)
        {
            return await _dao
                .Where(x => x.CreatorId == creatorId)
                .Include(x => x.UserInfo)
                .ToListAsync();
        }

        public async Task<Commission?> GetCommission(int commissionId)
        {
            return await _dao
                .Where(x=>x.CommissionId == commissionId)
                .Include(x => x.UserInfo)
                .SingleOrDefaultAsync();
        }

        public async Task<List<Commission>> GetUserCommissions(int userId)
        {
            return await _dao
                .Where(x => x.UserId == userId)
                .Include(x => x.UserInfo)
                .ToListAsync();
        }
        public async Task UpdateCommission(Commission commission)
        {
            await _dao.UpdateAsync(commission);
        }
    }
}
