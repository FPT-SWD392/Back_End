using BusinessObject;
using BusinessObject.SqlObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Services
{
    public interface ITransactionHistoryService
    {
        Task<List<TransactionHistory>> GetAllTransaction();
        Task<TransactionHistory?> GetTransactionHistoryById(int transactionId);
        Task<List<TransactionHistory>> GetUserTransactionHistories(int userId);
        Task CreateTransactionHistory(TransactionHistory transactionHistory);
        Task<List<TransactionHistory>> GetOnlyDepositTransactionByUser(int userId);
        Task<List<TransactionHistory>> GetAllDepositTransaction();
        Task<double> GetAmountDepositWithIn1Month(TransactionType type);
    }
}
