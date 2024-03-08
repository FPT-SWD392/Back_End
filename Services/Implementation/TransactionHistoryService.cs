using BusinessObject.SqlObject;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementation
{
    public class TransactionHistoryService : ITransactionHistoryService
    {
        private readonly ITransactionHistoryRepository _transactionHistoryRepository;

        public TransactionHistoryService(ITransactionHistoryRepository transactionHistoryRepository)
        {
            _transactionHistoryRepository = transactionHistoryRepository;
        }

        public async Task CreateTransactionHistory(TransactionHistory transactionHistory)
        {
            await _transactionHistoryRepository.CreateTransactionHistory(transactionHistory);
        }

        public async Task<List<TransactionHistory>> GetAllTransaction()
        {
            return await _transactionHistoryRepository.GetAllTransaction();
        }

        public async Task<TransactionHistory?> GetTransactionHistoryById(int transactionId)
        {
            return await _transactionHistoryRepository.GetTransactionHistoryById(transactionId);
        }

        public async Task<List<TransactionHistory>> GetUserTransactionHistories(int userId)
        {
            return await _transactionHistoryRepository.GetUserTransactionHistories(userId);
        }
    }
}
