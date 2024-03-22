using BusinessObject;
using BusinessObject.SqlObject;

namespace Repository.Interface
{
    public interface IReportRepository
    {
        public Task CreateNewReport(Report report);
        public Task DeleteReport(Report report);
        public Task UpdateReport(Report report);
        public Task<Report?> GetReportById(int reportId);
        public Task<List<Report>> GetUserReports(int userId);
        public Task<List<Report>> GetArtReports(int artId);
        public Task<List<Report>> GetComissionReports(int commissionId);
        public Task<List<Report>> GetCreatorReports(int creatorId);
        public Task<List<Report>> GetPostReports(int postId);

        public Task<List<Report>> GetAllUserReports();
        public Task<List<Report>> GetAllArtReports();
        public Task<List<Report>> GetAllComissionReports();
        public Task<List<Report>> GetAllCreatorReports();
        public Task<List<Report>> GetAllPostReports();
        public Task<List<Report>> GetAllReportsOfThatUser(int postId);
        public Task<List<Report>> GetAllReport();
        public Task<List<Report>> GetReportByReportedObjectTypeAndId(int id, ReportedObjectType type);
    }
}
