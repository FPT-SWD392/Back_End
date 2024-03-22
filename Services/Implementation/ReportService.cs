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
        public async Task<List<ReportReponse>?> GetReportWithCount(List<Report> list)
        {
            List<ReportReponse> reportReponses = new List<ReportReponse>();
            foreach (var report in list)
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
        public async Task<List<ReportReponse>?> GetAllReport()
        {
            var list = await _reportRepository.GetAllReport();
            return await GetReportWithCount(list);
        }
        public async Task<List<ReportReponse>?> GetAllArtReports()
        {
            var list = await _reportRepository.GetAllArtReports();
            return await GetReportWithCount(list);
        }

        public async Task<List<ReportReponse>?> GetAllPostReports()
        {
            var list = await _reportRepository.GetAllPostReports();
            return await GetReportWithCount(list);
        }
        public async Task<List<ReportReponse>?> GetAllCreatorReports()
        {
            var list = await _reportRepository.GetAllCreatorReports();
            return await GetReportWithCount(list);
        }
        public async Task<List<ReportReponse>?> GetAllComissionReports()
        {
            var list = await _reportRepository.GetAllComissionReports();
            return await GetReportWithCount(list);
        }
        public async Task<List<ReportReponse>?> GetAllUserReports()
        {
            var list = await _reportRepository.GetAllUserReports();
            return await GetReportWithCount(list);
        }

        public async Task<List<ReportReponse>?> GetArtReports(int artId)
        {
            var list = await _reportRepository.GetArtReports(artId);
            return await GetReportWithCount(list);
        }

        public async Task<List<ReportReponse>?> GetPostReports(int postId)
        {
            var list = await _reportRepository.GetPostReports(postId);
            return await GetReportWithCount(list);
        }
        public async Task<List<ReportReponse>?> GetCreatorReports(int creatorId)
        {
            var list = await _reportRepository.GetCreatorReports(creatorId);
            return await GetReportWithCount(list);
        }
        public async Task<List<ReportReponse>?> GetComissionReports(int comissionId)
        {
            var list = await _reportRepository.GetComissionReports(comissionId);
            return await GetReportWithCount(list);
        }
        public async Task<List<ReportReponse>?> GetUserReports(int userId)
        {
            var list = await _reportRepository.GetUserReports(userId);
            return await GetReportWithCount(list);
        }

        public async Task<List<ReportReponse>?> GetAllReportsOfThatUser(int userId)
        {
            var list = await _reportRepository.GetAllReportsOfThatUser(userId);
            return await GetReportWithCount(list);
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
