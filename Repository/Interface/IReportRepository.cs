using BusinessObject.SqlObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IReportRepository
    {
        public Task CreateNewReport(Report report);
        public Task DeleteReport(Report report);
        public Task UpdateReport(Report report);
        public Task<Report?> GetReportById(int reportId);
        public Task<List<Report>> GetAllUserReports(int userId);
        public Task<List<Report>> GetAllArtReports(int artId);
        public Task<List<Report>> GetAllComissionReports(int commissionId);
        public Task<List<Report>> GetAllCreatorReports(int creatorId);
        public Task<List<Report>> GetAllPostReports(int postId);
        public ISqlFluentRepository<Report> Query();
    }
}
