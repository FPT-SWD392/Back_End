using BusinessObject.DTO;
using BusinessObject.SqlObject;

namespace Services
{
    public interface IReportService
    {
        Task CreateReport(Report report);
        Task DeleteReport(int reportId);
        Task<List<Report>?> GetAllArtReports(int artId);
        Task<List<Report>?> GetAllPostReports(int postId);
        Task<List<Report>?> GetAllCreatorReports(int creatorId);
        Task<List<Report>?> GetAllComissionReports(int comissionId);
        Task<List<Report>?> GetAllUserReports(int userId);
        Task<List<Report>?> GetAllReportsOfThatUser(int userId);
        Task<Report?> GetReportById(int reportId);
        Task UpdateReport(Report report);
        Task<bool> ReportUser(ReportRequest report);
        Task<List<Report>?> GetAllReport();
        Task<bool> ReportCommission(ReportRequest report);
        Task<bool> ReportArt(ReportRequest report);
        Task<bool> ReportArtist(ReportRequest report);

    }
}
