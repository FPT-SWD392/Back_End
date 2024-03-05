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
        private IGenericDao<TransactionHistory> _dao;
        private ISqlFluentRepository<TransactionHistory> _sqlFluentRepository;
        public TransactionHistoryRepository(
            IDaoFactory daoFactory, 
            ISqlFluentRepository<TransactionHistory> sqlFluentRepository)
        {
            _dao = daoFactory.CreateDao<TransactionHistory>();
            _sqlFluentRepository = sqlFluentRepository;
        }

        public async Task CreateNewTransactionHistory(TransactionHistory transactionHistory)
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
                .Where(x => x.TransactionId == transactionId)
                .SingleOrDefaultAsync();
        }

        public async Task<List<TransactionHistory>> GetUserTransactionHistories(int userId)
        {
            return await _dao
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }

        public ISqlFluentRepository<TransactionHistory> Query()
        {
            return _sqlFluentRepository;
        }

        public async Task UpdateTransactionHistory(TransactionHistory transactionHistory)
        {
            await _dao.UpdateAsync(transactionHistory);
        }
    }
}
