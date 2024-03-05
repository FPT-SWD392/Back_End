using BusinessObject.SqlObject;
using DataAccessObject;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementation
{
    public class FollowRepository : IFollowRepository
    {
        private readonly IGenericDao<Follow> _dao;
        private readonly ISqlFluentRepository<Follow> _sqlFluentRepository;
        public FollowRepository(IDaoFactory daoFactory, ISqlFluentRepository<Follow> sqlFluentRepository)
        {
            _dao = daoFactory.CreateDao<Follow>();
            _sqlFluentRepository = sqlFluentRepository;
        }

        public async Task<long> CountArtistFollows(int creatorId)
        {
            return await _dao
                .Where(x => x.CreatorId == creatorId)
                .CountAsync();
        }

        public async Task CreateNewFollow(Follow follow)
        {
            await _dao.CreateAsync(follow);
        }

        public async Task DeleteFollow(Follow follow)
        {
            await _dao.DeleteAsync(follow);
        }

        public async Task<List<Follow>> GetAllUserFollows(int userId)
        {
            return await _dao
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }

        public async Task<List<Follow>> GetArtistFollowers(int creatorId)
        {
            return await _dao
                .Where(x => x.CreatorId == creatorId)
                .ToListAsync();
        }

        public async Task<Follow?> GetFollow(int userId, int creatorId)
        {
            return await _dao
                .Where(x => x.CreatorId == creatorId && x.UserId == userId)
                .SingleOrDefaultAsync();
        }

        public ISqlFluentRepository<Follow> Query()
        {
            return _sqlFluentRepository;
        }

        public async Task UpdateFollow(Follow follow)
        {
            await _dao.UpdateAsync(follow);
        }
    }
}
