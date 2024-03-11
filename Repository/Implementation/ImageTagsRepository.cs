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
        public ImageTagsRepository(IDaoFactory daoFactory)
        {
            _dao = daoFactory.CreateDao<ImageTags>();
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

        public async Task UpdateImageTags(ImageTags imageTags)
        {
            await _dao.UpdateAsync(imageTags);
        }
    }
}
