using BusinessObject;
using BusinessObject.DTO;
using BusinessObject.SqlObject;

namespace Services
{
    public interface IReportService
    {
        Task CreateReport(Report report);
        Task DeleteReport(int reportId);
        Task<List<ReportReponse>?> GetAllArtReports();
        Task<List<ReportReponse>?> GetAllPostReports();
        Task<List<ReportReponse>?> GetAllCreatorReports();
        Task<List<ReportReponse>?> GetAllComissionReports();
        Task<List<ReportReponse>?> GetAllUserReports();
        Task<List<ReportReponse>?> GetAllReportsOfThatUser(int userId);
        Task<Report?> GetReportById(int reportId);
        Task UpdateReport(Report report);
        Task<bool> ReportUser(ReportRequest report);
        Task<List<ReportReponse>?> GetAllReport();
        Task<bool> ReportCommission(ReportRequest report);
        Task<bool> ReportArt(ReportRequest report);
        Task<bool> ReportArtist(ReportRequest report);
        Task<List<ReportReponse>?> GetArtReports(int artId);
        Task<List<ReportReponse>?> GetPostReports(int postId);
        Task<List<ReportReponse>?> GetCreatorReports(int creatorId);
        Task<List<ReportReponse>?> GetComissionReports(int comissionId);
        Task<List<ReportReponse>?> GetUserReports(int userId);
        Task DeleteAfterBan(int id, ReportedObjectType type);
    }
}
