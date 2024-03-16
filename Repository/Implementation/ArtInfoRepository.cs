using BusinessObject.SqlObject;
using DataAccessObject;
using Repository.Interface;

namespace Repository.Implementation
{
    public class ArtInfoRepository : IArtInfoRepository
    {
        private readonly IGenericDao<ArtInfo> _artInfoDao;
        public ArtInfoRepository(IDaoFactory daoFactory)
        {
            _artInfoDao = daoFactory.CreateDao<ArtInfo>();
        }

        public async Task CreateNewArt(ArtInfo artInfo)
        {
            await _artInfoDao.CreateAsync(artInfo);
        }

        public async Task DeleteArt(ArtInfo artInfo)
        {
            await _artInfoDao.DeleteAsync(artInfo);
        }

        public async Task<List<ArtInfo>> GetAllArts()
        {
            return await _artInfoDao
                .Query()
                .ToListAsync();
        }

        public async Task<ArtInfo?> GetArtById(int id)
        {
            return await _artInfoDao
                .Query()
                .Where(x=>x.ArtId == id).SingleOrDefaultAsync();
        }

        public async Task UpdateArt(ArtInfo artInfo)
        {
            await _artInfoDao.UpdateAsync(artInfo);
        }
    }
}
