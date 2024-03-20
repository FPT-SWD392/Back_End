using BusinessObject.SqlObject;
using Microsoft.AspNetCore.Http;

namespace Services
{
    public interface ICommissionService
    {
        Task<(bool isCreated,string message)> CreateCommission(DateTime deadline, double price, int creatorId, int userId);
        Task<Commission?> GetUserCommission(int userId, int commissionId);
        Task<Commission?> GetCreatorCommission(int artistId, int commissionId);
        Task<List<Commission>> GetUserCommissions(int userId);
        Task<List<Commission>> GetCreatorCommissions(int artistId);
        Task<List<Commission>> GetCreatorAcceptedCommissions(int artistId);
        Task<(bool isUpdated, string message)> AcceptCommission(int creatorId, int commissionId);
        Task<(bool isUpdated, string message)> DenyCommission(int creatorId, int commissionId);
        Task<(bool isUpdated, string message)> FinishCommission(int creatorId , int commissionId, IFormFile image);
        Task<(bool isUpdated, string message)> CancelCommission(int userId, int commissionId);
        Task<(bool isUpdated, string message)> CreatorCancelCommission(int artistId, int commissionId);
    }
}
