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
        public Task CreateNewTransactionHistory(TransactionHistory transactionHistory);
        public Task DeleteTransactionHistory(TransactionHistory transactionHistory);
        public Task UpdateTransactionHistory(TransactionHistory transactionHistory);
        public Task<TransactionHistory?> GetTransactionHistoryById(int transactionId);
        public Task<List<TransactionHistory>> GetUserTransactionHistories(int userId);
        public ISqlFluentRepository<TransactionHistory> Query();
    }
}
