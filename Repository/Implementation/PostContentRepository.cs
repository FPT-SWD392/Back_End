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
    public class PostContentRepository : IPostContentRepository
    {
        private readonly IGenericDao<PostContent> _dao;
        public PostContentRepository( IDaoFactory daoFactory)
        {
            _dao = daoFactory.CreateDao<PostContent>();
        }

        public async Task CreateNewPostContent(PostContent postContent)
        {
            await _dao.CreateAsync(postContent);
        }

        public async Task DeletePostContent(PostContent postContent)
        {
            await _dao.DeleteAsync(postContent);
        }

        public async Task<PostContent?> GetPostContentById(int postId)
        {
            return await _dao
                .Query()
                .Where(x => x.PostId == postId)
                .SingleOrDefaultAsync();
        }

        public async Task UpdatePostContent(PostContent postContent)
        {
            await _dao.UpdateAsync(postContent);
        }
    }
}
