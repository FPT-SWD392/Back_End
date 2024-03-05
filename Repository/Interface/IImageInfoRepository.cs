using BusinessObject.NoSqlObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IImageInfoRepository
    {
        public Task CreateNewImageInfo(ImageInfo imageInfo);
        public Task DeleteImageInfo(ImageInfo imageInfo);
        public Task UpdateImageInfo(ImageInfo imageInfo);
        public Task<ImageInfo?> GetImageInfoById(int imageId);
        public IMongoFluentRepository<ImageInfo> Query();
    }
}
