using BusinessObject.SqlObject;
using DataAccessObject;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementation
{
    public class PostRepository : IPostRepository
    {
        private readonly IGenericDao<Post> _dao;
        public PostRepository(IDaoFactory daoFactory)
        {
            _dao = daoFactory.CreateDao<Post>();
        }

        public async Task CreateNewPost(Post post)
        {
            await _dao.CreateAsync(post);
        }

        public async Task DeletePost(Post post)
        {
            await _dao.DeleteAsync(post);
        }

        public async Task<List<Post>> GetAllArtistPosts(int creatorId)
        {
            return await _dao
                .Where(p=>p.CreatorId == creatorId)
                .ToListAsync();
        }

        public async Task<Post?> GetPostById(int postId)
        {
            return await _dao
                .Where(p => p.PostId == postId)
                .SingleOrDefaultAsync();
        }

        public async Task UpdatePost(Post post)
        {
            await _dao.UpdateAsync(post);
        }
    }
}
