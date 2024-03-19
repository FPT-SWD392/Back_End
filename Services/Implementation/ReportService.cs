using BusinessObject.DTO;
using BusinessObject.SqlObject;
using MongoDB.Driver;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementation
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;
        private readonly IUserInfoRepository _userInfoRepository;
        private readonly ICreatorInfoRepository _creatorInfoRepository;
        private readonly IArtInfoRepository _artInfoRepository;
        private readonly ICommissionRepository _commissionInfoRepository;
        private readonly IPostContentRepository _postInfoRepository;

        public ReportService(IUserInfoRepository userInfoRepository, IReportRepository reportRepository, 
                            ICreatorInfoRepository creatorInfoRepository, IPostContentRepository postInfoRepository, 
                            IArtInfoRepository artInfoRepository, ICommissionRepository commissionRepository)
        {
            _reportRepository = reportRepository;
            _userInfoRepository = userInfoRepository;
            _creatorInfoRepository = creatorInfoRepository;
            _postInfoRepository = postInfoRepository;
            _artInfoRepository = artInfoRepository;
            _commissionInfoRepository = commissionRepository;
        }

        public Task CreateReport(Report report)
        {
            return _reportRepository.CreateNewReport(report);
        }

        public async Task DeleteReport(int reportId)
        {
            var report = _reportRepository.GetReportById(reportId);
            await _reportRepository.DeleteReport(report.Result);
        }
        public async Task<List<Report>?> GetAllReport()
        {
            return await _reportRepository.GetAllReport();
        }
        public async Task<List<Report>?> GetAllArtReports(int artId)
        {
            return await _reportRepository.GetAllArtReports(artId);
        }

        public async Task<List<Report>?> GetAllPostReports(int postId)
        {
            return await _reportRepository.GetAllPostReports(postId);
        }
        public async Task<List<Report>?> GetAllCreatorReports(int creatorId)
        {
            return await _reportRepository.GetAllCreatorReports(creatorId);
        }
        public async Task<List<Report>?> GetAllComissionReports(int comissionId)
        {
            return await _reportRepository.GetAllComissionReports(comissionId);
        }
        public async Task<List<Report>?> GetAllUserReports(int userId)
        {
            return await _reportRepository.GetAllUserReports(userId);
        }
        public async Task<List<Report>?> GetAllReportsOfThatUser(int userId)
        {
            return await _reportRepository.GetAllReportsOfThatUser(userId);
        }
        public async Task<Report?> GetReportById(int reportId)
        {
            return await _reportRepository.GetReportById(reportId);
        }

        public async Task UpdateReport(Report report)
        {
            await _reportRepository.UpdateReport(report);
        }

        public async Task<bool> ReportUser(ReportRequest report)
        {
            var user = await _userInfoRepository.GetUserById(report.ReportedObjectId);
            if (user == null)
            {
                return false;
            }
            else
            {
                Report newreport = new Report()
                {
                    ReportDescription = report.Description,
                    ReportReason = report.Reason,
                    ReportDate = report.ReportDate,
                    ReportedObjectId = report.ReportedObjectId,
                    ReportedObjectType = report.ReportedObjectType,
                    ReporterId = report.ReporterId
                };
                await _reportRepository.CreateNewReport(newreport);
                return true;
            }
        }
        public async Task<bool> ReportArtist(ReportRequest report)
        {
            var creator = await _creatorInfoRepository.GetCreatorInfo(report.ReportedObjectId);
            if (creator == null)
            {
                return false;
            }
            else
            {
                Report newreport = new Report()
                {
                    ReportDescription = report.Description,
                    ReportReason = report.Reason,
                    ReportDate = report.ReportDate,
                    ReportedObjectId = report.ReportedObjectId,
                    ReportedObjectType = report.ReportedObjectType,
                    ReporterId = report.ReporterId
                };
                await _reportRepository.CreateNewReport(newreport);
                return true;
            }
        }

        public async Task<bool> ReportArt(ReportRequest report)
        {
            var art = await _artInfoRepository.GetArtById(report.ReportedObjectId);
            if (art == null)
            {
                return false;
            }
            else
            {
                Report newreport = new Report()
                {
                    ReportDescription = report.Description,
                    ReportReason = report.Reason,
                    ReportDate = report.ReportDate,
                    ReportedObjectId = report.ReportedObjectId,
                    ReportedObjectType = report.ReportedObjectType,
                    ReporterId = report.ReporterId
                };
                await _reportRepository.CreateNewReport(newreport);
                return true;
            }
        }
        public async Task<bool> ReportPost(ReportRequest report)
        {
            var post = await _postInfoRepository.GetPostContentById(report.ReportedObjectId);
            if (post == null)
            {
                return false;
            }
            else
            {
                Report newreport = new Report()
                {
                    ReportDescription = report.Description,
                    ReportReason = report.Reason,
                    ReportDate = report.ReportDate,
                    ReportedObjectId = report.ReportedObjectId,
                    ReportedObjectType = report.ReportedObjectType,
                    ReporterId = report.ReporterId
                };
                await _reportRepository.CreateNewReport(newreport);
                return true;
            }
        }
        public async Task<bool> ReportCommission(ReportRequest report)
        {
            var commission = await _commissionInfoRepository.GetCommission(report.ReportedObjectId);
            if (commission == null)
            {
                return false;
            }
            else
            {
                Report newreport = new Report()
                {
                    ReportDescription = report.Description,
                    ReportReason = report.Reason,
                    ReportDate = report.ReportDate,
                    ReportedObjectId = report.ReportedObjectId,
                    ReportedObjectType = report.ReportedObjectType,
                    ReporterId = report.ReporterId
                };
                await _reportRepository.CreateNewReport(newreport);
                return true;
            }
        }
    }
}
