using BusinessObject.MongoDbObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IPostContentRepository
    {
        public Task CreateNewPostContent(PostContent postContent);
        public Task DeletePostContent(PostContent postContent);
        public Task UpdatePostContent(PostContent postContent);
        public Task<PostContent?> GetPostContentById(int postId);
    }
}
