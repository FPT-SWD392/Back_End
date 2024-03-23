using BusinessObject;
using BusinessObject.SqlObject;
using Repository.Implementation;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementation
{
    public class CommissionService : ICommissionService
    {
        private readonly IUserInfoRepository _userRepository;
        private readonly ICommissionRepository _commissionRepository;
        private readonly ITransactionHistoryRepository _transactionHistoryRepository;
        public CommissionService(ICommissionRepository commissionRepository, IUserInfoRepository userInfoRepository, ITransactionHistoryRepository transactionHistoryRepository)
        {
            _commissionRepository = commissionRepository;
            _userRepository = userInfoRepository;
            _transactionHistoryRepository = transactionHistoryRepository;
        }

        public async Task CreateCommission(DateTime deadline, double price, int creatorId, int userId)
        {
            try
            {
                UserInfo user = await _userRepository.GetUserById(userId);
                if (user.Balance >= price)
                {
                    Commission newCommission = new Commission
                    {
                        CreatedDate = DateTime.Today,
                        Deadline = deadline,
                        Price = price,
                        CreatorId = creatorId,
                        UserId = userId,
                        CommissionStatus = CommissionStatus.Pending
                    };
                    await _commissionRepository.CreateNewCommission(newCommission);

                    user.Balance = user.Balance - price;

                    await _userRepository.UpdateUser(user);
                    TransactionHistory transactionHistory = new TransactionHistory
                    {
                        UserId = userId,
                        Note = "",
                        Amount = price,
                        TransactionDate = DateTime.Today,
                        TransactionType = TransactionType.RequestCommission
                    };
                    await _transactionHistoryRepository.CreateTransactionHistory(transactionHistory);
                }
                else
                {
                    throw new Exception("There's not enough money in your account");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Commission?> GetCommissionByCommissionId(int commissionId)
        {
            return await _commissionRepository.GetCommission(commissionId);
        }

        public async Task<List<Commission?>> GetCommissionByUserId(int userId)
        {
            return await _commissionRepository.GetUserCommissions(userId);
        }

        public async Task<List<Commission>?> GetCommissionByCreatorId(int artistId)
        {
            UserInfo creator = await _userRepository.GetUserById(artistId);
            return await _commissionRepository.GetArtistCommissions((int)creator.CreatorId);
        }

        public async Task<List<Commission>?> GetAcceptedCommissionByCreatorId(int artistId)
        {
            UserInfo creator = await _userRepository.GetUserById(artistId);
            return await _commissionRepository.GetAcceptedCommissionByCreatorId((int)creator.CreatorId);
        }

        public async Task AcceptCommission(int commissionId)
        {
            try
            {
                Commission commission = await _commissionRepository.GetCommission(commissionId);
                if (commission != null)
                {
                    commission.CommissionStatus = CommissionStatus.Accepted;
                    await _commissionRepository.UpdateCommission(commission);
                }
                else
                {
                    throw new Exception("Cannot find commission");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DenyCommission(int commissionId)
        {
            try
            {
                Commission commission = await _commissionRepository.GetCommission(commissionId);
                if (commission != null)
                {
                    commission.CommissionStatus = CommissionStatus.Denied;
                    await _commissionRepository.UpdateCommission(commission);

                    UserInfo user = await _userRepository.GetUserById(commission.UserId);
                    user.Balance = user.Balance + commission.Price;
                    await _userRepository.UpdateUser(user);
                    TransactionHistory transactionHistory = new TransactionHistory
                    {
                        UserId = commission.UserId,
                        Note = "",
                        Amount = commission.Price,
                        TransactionDate = DateTime.Today,
                        TransactionType = TransactionType.RequestCommissionDeny
                    };
                    await _transactionHistoryRepository.CreateTransactionHistory(transactionHistory);
                }
                else
                {
                    throw new Exception("Cannot find commission");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task CancelCommission(int commissionId)
        {
            try
            {
                Commission commission = await _commissionRepository.GetCommission(commissionId);
                if (commission != null)
                {
                    commission.CommissionStatus = CommissionStatus.Canceled;
                    await _commissionRepository.UpdateCommission(commission);

                    UserInfo user = await _userRepository.GetUserById(commission.UserId);
                    user.Balance = user.Balance + commission.Price / 2;
                    await _userRepository.UpdateUser(user);
                    TransactionHistory transactionHistory = new TransactionHistory
                    {
                        UserId = commission.UserId,
                        Note = "",
                        Amount = commission.Price / 2,
                        TransactionDate = DateTime.Today,
                        TransactionType = TransactionType.RequestCommissionCancel
                    };
                    await _transactionHistoryRepository.CreateTransactionHistory(transactionHistory);
                }
                else
                {
                    throw new Exception("Cannot find commission");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task FinishCommission(int commissionId, int ImageId)
        {
            try
            {
                Commission commission = await _commissionRepository.GetCommission(commissionId);
                if (commission != null)
                {
                    commission.CommissionStatus = CommissionStatus.Finished;
                    commission.ImageId = ImageId;
                    await _commissionRepository.UpdateCommission(commission);
                }
                else
                {
                    throw new Exception("Cannot find commission");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
