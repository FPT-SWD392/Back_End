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
    public class ImageInfoRepository : IImageInfoRepository
    {
        private readonly IGenericDao<ImageInfo> _dao;
        public ImageInfoRepository(IDaoFactory daoFactory)
        {
            _dao = daoFactory.CreateDao<ImageInfo>();
        }

        public async Task CreateNewImageInfo(ImageInfo imageInfo)
        {
            await _dao.CreateAsync(imageInfo);
        }

        public async Task DeleteImageInfo(ImageInfo imageInfo)
        {
            await _dao.DeleteAsync(imageInfo);
        }

        public async Task<ImageInfo?> GetImageInfoById(int imageId)
        {
            return await _dao
                .Query()
                .Where(x => x.ImageId == imageId)
                .SingleOrDefaultAsync();
        }

        public async Task UpdateImageInfo(ImageInfo imageInfo)
        {
            await _dao.UpdateAsync(imageInfo);
        }
    }
}
