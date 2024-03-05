﻿using BusinessObject.SqlObject;
using DataAccessObject;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementation
{
    public class CommissionRepository : ICommissionRepository
    {
        private IGenericDao<Commission> _dao;
        private ISqlFluentRepository<Commission> _sqlFluentRepository;
        public CommissionRepository(IDaoFactory daoFactory, ISqlFluentRepository<Commission> sqlFluentRepository)
        {
            _dao = daoFactory.CreateDao<Commission>();
            _sqlFluentRepository = sqlFluentRepository;
        }
        public async Task CreateNewCommission(Commission commission)
        {
            await _dao.CreateAsync(commission);
        }

        public async Task DeleteCommission(Commission commission)
        {
            await _dao.DeleteAsync(commission);
        }

        public async Task<List<Commission>> GetArtistCommissions(int creatorId)
        {
            return await _dao
                .Where(x=>x.CreatorId == creatorId)
                .ToListAsync();
        }

        public async Task<Commission?> GetCommission(int commissionId)
        {
            return await _dao
                .Where(x=>x.CommissionId == commissionId)
                .SingleOrDefaultAsync();
        }

        public async Task<List<Commission>> GetUserCommissions(int userId)
        {
            return await _dao
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }

        public ISqlFluentRepository<Commission> Query()
        {
            return _sqlFluentRepository;
        }

        public async Task UpdateCommission(Commission commission)
        {
            await _dao.UpdateAsync(commission);
        }
    }
}