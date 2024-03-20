using BusinessObject.SqlObject;
using DataAccessObject;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Repository.Implementation
{
    public class TransactionHistoryRepository : ITransactionHistoryRepository
    {
        private readonly IGenericDao<TransactionHistory> _dao;
        public TransactionHistoryRepository(IDaoFactory daoFactory)
        {
            _dao = daoFactory.CreateDao<TransactionHistory>();
        }
        public async Task<List<TransactionHistory>> GetAllTransaction()
        {
            return await _dao
                .Query()
                .ToListAsync();
        }

        public async Task CreateTransactionHistory(TransactionHistory transactionHistory)
        {
            await _dao.CreateAsync(transactionHistory);
        }

        public async Task DeleteTransactionHistory(TransactionHistory transactionHistory)
        {
            await _dao.DeleteAsync(transactionHistory);
        }

        public async Task<TransactionHistory?> GetTransactionHistoryById(int transactionId)
        {
            return await _dao
                .Query()
                .Where(x => x.TransactionId == transactionId)
                .SingleOrDefaultAsync();
        }

        public async Task<List<TransactionHistory>> GetUserTransactionHistories(int userId)
        {
            return await _dao
                .Query()
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }

        public async Task UpdateTransactionHistory(TransactionHistory transactionHistory)
        {
            await _dao.UpdateAsync(transactionHistory);
        }

        public async Task<List<TransactionHistory>> GetOnlyDepositTransactionByUser(int userId)
        {
            return await _dao
                .Query()
                .Where(u => u.UserId == userId && (u.TransactionType == BusinessObject.TransactionType.DepositVnPay
                                                    || u.TransactionType == BusinessObject.TransactionType.DepositMomo
                                                    || u.TransactionType == BusinessObject.TransactionType.DepositOther
                                                    || u.TransactionType == BusinessObject.TransactionType.DepositManualAdmin))
                .ToListAsync();
                        
        }
        public async Task<List<TransactionHistory>> GetAllDepositTransaction()
        {
            return await _dao
                .Query()
                .Where(u => u.TransactionType == BusinessObject.TransactionType.DepositVnPay
                                                    || u.TransactionType == BusinessObject.TransactionType.DepositMomo
                                                    || u.TransactionType == BusinessObject.TransactionType.DepositOther
                                                    || u.TransactionType == BusinessObject.TransactionType.DepositManualAdmin)
                .ToListAsync();

        }
    }
}
