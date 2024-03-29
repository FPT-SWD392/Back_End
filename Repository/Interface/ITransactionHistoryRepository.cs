﻿using BusinessObject;
using BusinessObject.SqlObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface ITransactionHistoryRepository
    {
        public Task<List<TransactionHistory>> GetAllTransaction();
        public Task CreateTransactionHistory(TransactionHistory transactionHistory);
        public Task DeleteTransactionHistory(TransactionHistory transactionHistory);
        public Task UpdateTransactionHistory(TransactionHistory transactionHistory);
        public Task<TransactionHistory?> GetTransactionHistoryById(int transactionId);
        public Task<List<TransactionHistory>> GetUserTransactionHistories(int userId);
        public Task<List<TransactionHistory>> GetOnlyDepositTransactionByUser(int userId);
        public Task<List<TransactionHistory>> GetAllDepositTransaction();
        public Task<List<TransactionHistory>> GetTransactionHistoryByTransactionTypeWithInLastMonth(TransactionType type);
        public Task<List<TransactionHistory>> GetAllTransactionThisUserByStatus(int userId, TransactionType transactionType);
    }
}
