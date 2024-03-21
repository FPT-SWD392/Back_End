using BusinessObject;
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
        public async Task<List<ReportReponse>?> GetAllReport()
        {
            List<ReportReponse> reportReponses = new List<ReportReponse>();
            var test = await _reportRepository.GetAllReport();
            foreach (var report in test)
            {
                bool foundMatchingResponse = false;
                if (reportReponses.Count() == 0)
                {
                    ReportReponse response = new ReportReponse()
                    {
                        Reason = report.ReportReason,
                        Description = report.ReportDescription,
                        ReportDate = report.ReportDate,
                        ReportedObjectId = report.ReportedObjectId,
                        ReporterId = report.ReporterId,
                        ReportedObjectType = report.ReportedObjectType,
                        Count = 1
                    };
                    reportReponses.Add(response);
                }
                else
                {
                    for (int i = 0; i < reportReponses.Count(); i++)
                    {
                        if (report.ReportedObjectId == reportReponses[i].ReportedObjectId &&
                            report.ReportedObjectType == reportReponses[i].ReportedObjectType)
                        {
                            
                            reportReponses[i].Count += 1;
                            foundMatchingResponse = true;
                            break;
                        }
                        
                    }
                    if (!foundMatchingResponse)
                    {
                        ReportReponse response = new ReportReponse()
                        {
                            Reason = report.ReportReason,
                            Description = report.ReportDescription,
                            ReportDate = report.ReportDate,
                            ReportedObjectId = report.ReportedObjectId,
                            ReporterId = report.ReporterId,
                            ReportedObjectType = report.ReportedObjectType,
                            Count = 1
                        };
                        reportReponses.Add(response);
                    }
                }
            }
            return reportReponses;
        }
        public async Task<List<Report>?> GetAllArtReports()
        {
            return await _reportRepository.GetAllArtReports();
        }

        public async Task<List<Report>?> GetAllPostReports()
        {
            return await _reportRepository.GetAllPostReports();
        }
        public async Task<List<Report>?> GetAllCreatorReports()
        {
            return await _reportRepository.GetAllCreatorReports();
        }
        public async Task<List<Report>?> GetAllComissionReports()
        {
            return await _reportRepository.GetAllComissionReports();
        }
        public async Task<List<Report>?> GetAllUserReports()
        {
            return await _reportRepository.GetAllUserReports();
        }

        public async Task<List<Report>?> GetArtReports(int artId)
        {
            return await _reportRepository.GetArtReports(artId);
        }

        public async Task<List<Report>?> GetPostReports(int postId)
        {
            return await _reportRepository.GetPostReports(postId);
        }
        public async Task<List<Report>?> GetCreatorReports(int creatorId)
        {
            return await _reportRepository.GetCreatorReports(creatorId);
        }
        public async Task<List<Report>?> GetComissionReports(int comissionId)
        {
            return await _reportRepository.GetComissionReports(comissionId);
        }
        public async Task<List<Report>?> GetUserReports(int userId)
        {
            return await _reportRepository.GetUserReports(userId);
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

        public async Task DeleteAfterBan(int id, ReportedObjectType type)
        {
            var delete = await _reportRepository.GetReportByReportedObjectTypeAndId(id, type);
            foreach (var report in delete)
            {
                report.ReportedObjectType = ReportedObjectType.Banned;
                await _reportRepository.UpdateReport(report);
            }
        }
    }
}
