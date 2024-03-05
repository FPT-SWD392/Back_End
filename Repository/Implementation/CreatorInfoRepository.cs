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
    public class CreatorInfoRepository : ICreatorInfoRepository
    {
        private IGenericDao<CreatorInfo> _dao;
        private ISqlFluentRepository<CreatorInfo> _sqlFluentRepository;
        public CreatorInfoRepository(IDaoFactory daoFactory, ISqlFluentRepository<CreatorInfo> sqlFluentRepository)
        {
            _dao = daoFactory.CreateDao<CreatorInfo>();
            _sqlFluentRepository = sqlFluentRepository;
        }
        public async Task CreateNewCreatorInfo(CreatorInfo creatorInfo)
        {
            await _dao.CreateAsync(creatorInfo);
        }

        public async Task<List<CreatorInfo>> GetAllCreatorInfo()
        {
            return await _dao.ToListAsync();
        }

        public async Task<CreatorInfo?> GetCreatorInfo(int creatorId)
        {
            return await _dao
                .Where(x=>x.CreatorId == creatorId)
                .SingleOrDefaultAsync();
        }

        public ISqlFluentRepository<CreatorInfo> Query()
        {
            return _sqlFluentRepository;
        }

        public async Task UpdateCreatorInfo(CreatorInfo creatorInfo)
        {
            await _dao.UpdateAsync(creatorInfo);
        }
    }
}