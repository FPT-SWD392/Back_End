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
    public class ImageTagsRepository : IImageTagsRepository
    {
        private readonly IGenericDao<ImageTags> _dao;
        private readonly IMongoFluentRepository<ImageTags> _mongoFluentRepository;
        public ImageTagsRepository(
            IDaoFactory daoFactory, 
            IMongoFluentRepository<ImageTags> mongoFluentRepository)
        {
            _dao = daoFactory.CreateDao<ImageTags>();
            _mongoFluentRepository = mongoFluentRepository;
        }

        public async Task CreateNewImageTags(ImageTags imageTags)
        {
            await _dao.CreateAsync(imageTags);
        }

        public async Task DeleteImageTags(ImageTags imageTags)
        {
            await _dao.DeleteAsync(imageTags);
        }

        public async Task<ImageTags?> GetImageTagsById(int imageId)
        {
            return await _dao
                .Where(x => x.ImageId == imageId)
                .SingleOrDefaultAsync();
        }

        public IMongoFluentRepository<ImageTags> Query()
        {
            return _mongoFluentRepository;
        }

        public async Task UpdateImageTags(ImageTags imageTags)
        {
            await _dao.UpdateAsync(imageTags);
        }
    }
}
