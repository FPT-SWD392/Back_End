using BusinessObject;
using BusinessObject.SqlObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface ICommissionService
    {
        Task/*<Commission?>*/ CreateCommission(DateTime deadline, double price, int creatorId, int userId);
        Task<Commission?> GetCommissionByCommissionId(int commissionId);
        Task<List<Commission>?> GetCommissionByUserId(int userId);
        Task<List<Commission>?> GetCommissionByCreatorId(int artistId);
        Task<List<Commission>?> GetAcceptedCommissionByCreatorId(int artistId);
        //Task UpdateCommissionStatus(int commissionId, CommissionStatus commissionStatus);
        Task UpdateCommissionStatus(int commissionId, string status);
        Task FinishCommission(int commissionId, int ImageId);
    }
}
