﻿using BusinessObject.DTO;
using BusinessObject.SqlObject;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementation
{
    public class CreatorInfoService : ICreatorInfoService
    {
        private readonly ICreatorInfoRepository _creatorInfoRepository;
        private readonly IUserInfoRepository _userInfoRepository;
        private readonly ITransactionHistoryRepository _transactionHistoryRepository;
        private double _priceToUpgrade = default!;
        public CreatorInfoService(ICreatorInfoRepository creatorInfoRepository, 
                                    IUserInfoRepository userInfoRepository, 
                                    IConfiguration configuration,
                                    ITransactionHistoryRepository transactionHistoryRepository)
        {
            _creatorInfoRepository = creatorInfoRepository;
            _userInfoRepository = userInfoRepository;
            string priceToUpgradeString = configuration["AppSetting:PremiumPrice"];
            _priceToUpgrade = double.Parse(priceToUpgradeString);
            _transactionHistoryRepository = transactionHistoryRepository;
        }

        public async Task UpgradeToCreatorWithBalance(int userId, CreatorInfoDTO creatorInfoDTO)
        {
            var user = await _userInfoRepository.GetUserById(userId);
            if (user.CreatorId != null)
            {
                throw new Exception("You have already been a creator.");
            }
            else
            {
                if (user.Balance < _priceToUpgrade)
                {
                    throw new Exception("You don't have enough money in your account.");
                }
                #region Create Creator Info
                var creatorInfo = new CreatorInfo()
                {
                    BecomeArtistDate = DateTime.UtcNow,
                    AcceptCommissionStatus = BusinessObject.AcceptCommissionStatus.Closed,
                    Bio = creatorInfoDTO.Bio,
                    ContactInfo = creatorInfoDTO.ContactInfo,                   
                };
                await _creatorInfoRepository.CreateNewCreatorInfo(creatorInfo);
                #endregion
                #region Update Balance and Creator Id
                if (creatorInfo.CreatorId.ToString().IsNullOrEmpty())
                {
                    throw new Exception("Experiment failed.");
                }
                else
                {
                    user.Balance -= _priceToUpgrade;
                    user.CreatorId = creatorInfo.CreatorId;
                    await _userInfoRepository.UpdateUser(user);
                }
                #endregion
                #region Add transaction history for this user
                var transactionHistoryBuyer = new TransactionHistory()
                {
                    UserId = userId,
                    Note = "You upgraded to creator",
                    Amount = _priceToUpgrade,
                    TransactionType = BusinessObject.TransactionType.UpgradeToCreator,
                    TransactionDate = DateTime.UtcNow,
                };

                await _transactionHistoryRepository.CreateTransactionHistory(transactionHistoryBuyer);
                #endregion
            }

        }
        public async Task UpgradeToCreatorWithExternalParty(int userId, CreatorInfoDTO creatorInfoDTO)
        {
            var user = await _userInfoRepository.GetUserById(userId);
            if (user.CreatorId != null)
            {
                throw new Exception("You have already been a creator.");
            }
            else
            {
                #region Create Creator Info
                var creatorInfo = new CreatorInfo()
                {
                    BecomeArtistDate = DateTime.UtcNow,
                    AcceptCommissionStatus = BusinessObject.AcceptCommissionStatus.Closed,
                    Bio = creatorInfoDTO.Bio,
                    ContactInfo = creatorInfoDTO.ContactInfo,
                };
                await _creatorInfoRepository.CreateNewCreatorInfo(creatorInfo);
                #endregion
                #region Update Creator Id
                if (creatorInfo.CreatorId.ToString().IsNullOrEmpty())
                {
                    throw new Exception("Experiment failed.");
                }
                else
                {
                    user.CreatorId = creatorInfo.CreatorId;
                    await _userInfoRepository.UpdateUser(user);
                }
                #endregion
                #region Add transaction history for this user
                var transactionHistoryBuyer = new TransactionHistory()
                {
                    UserId = userId,
                    Note = "You upgraded to creator",
                    Amount = _priceToUpgrade,
                    TransactionType = BusinessObject.TransactionType.UpgradeToCreator,
                    TransactionDate = DateTime.UtcNow,
                };

                await _transactionHistoryRepository.CreateTransactionHistory(transactionHistoryBuyer);
                #endregion
            }
        }
    }
}
