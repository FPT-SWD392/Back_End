using BusinessObject.NoSqlObject;
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
        private IGenericDao<ImageInfo> _dao;
        private IMongoFluentRepository<ImageInfo> _mongoFluentRepository;
        public ImageInfoRepository(
            IDaoFactory daoFactory, 
            IMongoFluentRepository<ImageInfo> mongoFluentRepository)
        {
            _dao = daoFactory.CreateDao<ImageInfo>();
            _mongoFluentRepository = mongoFluentRepository;
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
                .Where(x => x.ImageId == imageId)
                .SingleOrDefaultAsync();
        }

        public IMongoFluentRepository<ImageInfo> Query()
        {
            return _mongoFluentRepository;
        }

        public async Task UpdateImageInfo(ImageInfo imageInfo)
        {
            await _dao.UpdateAsync(imageInfo);
        }
    }
}
