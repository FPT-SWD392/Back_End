using BusinessObject.MongoDbObject;
using DataAccessObject;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementation
{
    public class CommissionImageInfoRepository : ICommissionImageInfoRepository
    {
        private readonly IGenericDao<CommissionImageInfo> _dao;
        public CommissionImageInfoRepository(IDaoFactory daoFactory)
        {
            _dao = daoFactory.CreateDao<CommissionImageInfo>();
        }

        public async Task CreateNewImageInfo(CommissionImageInfo imageInfo)
        {
            await _dao.CreateAsync(imageInfo);
        }

        public async Task DeleteImageInfo(CommissionImageInfo imageInfo)
        {
            await _dao.DeleteAsync(imageInfo);
        }

        public async Task<CommissionImageInfo?> GetImageInfoById(string image)
        {
            return await _dao
                .Query()
                .Where(x => x.ImageId == image)
                .SingleOrDefaultAsync();
        }

        public async Task UpdateImageInfo(CommissionImageInfo imageInfo)
        {
            await _dao.UpdateAsync(imageInfo);
        }
    }
}
