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
    public class PostContentRepository : IPostContentRepository
    {
        private readonly IGenericDao<PostContent> _dao;
        private readonly IMongoFluentRepository<PostContent> _mongoFluentRepository;
        public PostContentRepository(
            IDaoFactory daoFactory, 
            IMongoFluentRepository<PostContent> mongoFluentRepository)
        {
            _dao = daoFactory.CreateDao<PostContent>();
            _mongoFluentRepository = mongoFluentRepository;
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
                .Where(x => x.PostId == postId)
                .SingleOrDefaultAsync();
        }

        public IMongoFluentRepository<PostContent> Query()
        {
            return _mongoFluentRepository;
        }

        public async Task UpdatePostContent(PostContent postContent)
        {
            await _dao.UpdateAsync(postContent);
        }
    }
}
