using BusinessObject;
using BusinessObject.DTO;
using BusinessObject.SqlObject;

namespace Services
{
    public interface IReportService
    {
        Task CreateReport(Report report);
        Task DeleteReport(int reportId);
        Task<List<Report>?> GetAllArtReports();
        Task<List<Report>?> GetAllPostReports();
        Task<List<Report>?> GetAllCreatorReports();
        Task<List<Report>?> GetAllComissionReports();
        Task<List<Report>?> GetAllUserReports();
        Task<List<Report>?> GetAllReportsOfThatUser(int userId);
        Task<Report?> GetReportById(int reportId);
        Task UpdateReport(Report report);
        Task<bool> ReportUser(ReportRequest report);
        Task<List<ReportReponse>?> GetAllReport();
        Task<bool> ReportCommission(ReportRequest report);
        Task<bool> ReportArt(ReportRequest report);
        Task<bool> ReportArtist(ReportRequest report);
        public Task<List<Report>?> GetArtReports(int artId);
        public Task<List<Report>?> GetPostReports(int postId);
        public Task<List<Report>?> GetCreatorReports(int creatorId);
        public Task<List<Report>?> GetComissionReports(int comissionId);
        public Task<List<Report>?> GetUserReports(int userId);
        public Task DeleteAfterBan(int id, ReportedObjectType type);
    }
}
