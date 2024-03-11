using BusinessObject.SqlObject;
using DataAccessObject;
using Repository.Interface;

namespace Repository.Implementation
{
    public class CreatorInfoRepository : ICreatorInfoRepository
    {
        private readonly IGenericDao<CreatorInfo> _dao;
        public CreatorInfoRepository(IDaoFactory daoFactory)
        {
            _dao = daoFactory.CreateDao<CreatorInfo>();
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
                .Include(x => x.UserInfo)
                .SingleOrDefaultAsync();
        }

        public async Task UpdateCreatorInfo(CreatorInfo creatorInfo)
        {
            await _dao.UpdateAsync(creatorInfo);
        }
    }
}
