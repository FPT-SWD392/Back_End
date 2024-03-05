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
    public class ArtInfoRepository : IArtInfoRepository
    {
        private readonly IGenericDao<ArtInfo> _artInfoDao;
        private readonly ISqlFluentRepository<ArtInfo> _sqlFluentRepository;
        public ArtInfoRepository(IDaoFactory daoFactory, ISqlFluentRepository<ArtInfo> sqlFluentRepository)
        {
            _artInfoDao = daoFactory.CreateDao<ArtInfo>();
            _sqlFluentRepository = sqlFluentRepository;
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
            return await _artInfoDao.ToListAsync();
        }

        public async Task<ArtInfo?> GetArtById(int id)
        {
            return await _artInfoDao.Where(x=>x.ArtId == id).SingleOrDefaultAsync();
        }

        public ISqlFluentRepository<ArtInfo> Query()
        {
            return _sqlFluentRepository;
        }

        public async Task UpdateArt(ArtInfo artInfo)
        {
            await _artInfoDao.UpdateAsync(artInfo);
        }
    }
}
