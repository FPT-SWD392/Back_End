using BusinessObject.NoSqlObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IImageTagsRepository
    {
        public Task CreateNewImageTags(ImageTags imageTags);
        public Task DeleteImageTags(ImageTags imageTags);
        public Task UpdateImageTags(ImageTags imageTags);
        public Task<ImageTags?> GetImageTagsById(int imageId);
    }
}
