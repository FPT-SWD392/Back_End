using BusinessObject.MongoDbObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface ICommissionImageInfoRepository
    {
        public Task CreateNewImageInfo(CommissionImageInfo imageInfo);
        public Task DeleteImageInfo(CommissionImageInfo imageInfo);
        public Task UpdateImageInfo(CommissionImageInfo imageInfo);
        public Task<CommissionImageInfo?> GetImageInfoById(int imageId);
    }
}
