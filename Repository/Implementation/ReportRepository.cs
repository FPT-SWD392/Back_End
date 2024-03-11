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
    public class ReportRepository : IReportRepository
    {
        private readonly IGenericDao<Report> _dao;
        public ReportRepository(IDaoFactory daoFactory)
        {
            _dao = daoFactory.CreateDao<Report>();
        }

        public async Task CreateNewReport(Report report)
        {
            await _dao.CreateAsync(report);
        }

        public async Task DeleteReport(Report report)
        {
            await _dao.DeleteAsync(report);
        }

        public async Task<List<Report>> GetAllArtReports(int artId)
        {
            return await _dao
                .Where(x=>x.ReportedArtId == artId)
                .ToListAsync();
        }

        public async Task<List<Report>> GetAllComissionReports(int commissionId)
        {
            return await _dao
                .Where(x => x.ReportedCommissionId == commissionId)
                .ToListAsync();
        }

        public async Task<List<Report>> GetAllCreatorReports(int creatorId)
        {
            return await _dao
                .Where(x => x.ReportedCreatorId == creatorId)
                .ToListAsync();
        }

        public async Task<List<Report>> GetAllPostReports(int postId)
        {
            return await _dao
                .Where(x => x.ReportedPostId == postId)
                .ToListAsync();
        }

        public async Task<List<Report>> GetAllUserReports(int userId)
        {
            return await _dao
                .Where(x => x.ReporterId == userId)
                .ToListAsync();
        }

        public async Task<Report?> GetReportById(int reportId)
        {
            return await _dao
                .Where(x => x.ReporterId == reportId)
                .SingleOrDefaultAsync();
        }

        public async Task UpdateReport(Report report)
        {
            await _dao.UpdateAsync(report);
        }
    }
}
