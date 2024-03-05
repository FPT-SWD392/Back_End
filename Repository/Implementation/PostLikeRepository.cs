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
    public class PostLikeRepository : IPostLikeRepository
    {
        private IGenericDao<PostLike> _dao;
        private ISqlFluentRepository<PostLike> _sqlFluentRepository;
        public PostLikeRepository(IDaoFactory daoFactory, ISqlFluentRepository<PostLike> sqlFluentRepository)
        {
            _dao = daoFactory.CreateDao<PostLike>();
            _sqlFluentRepository = sqlFluentRepository;
        }

        public async Task<long> CountPostLikes(int postId)
        {
            return await _dao
                .Where(x=>x.PostId==postId)
                .CountAsync();
        }

        public async Task CreateNewPostLike(PostLike postLike)
        {
            await _dao.CreateAsync(postLike);
        }

        public async Task DeletePostLike(PostLike postLike)
        {
            await _dao.DeleteAsync(postLike);
        }

        public async Task<List<PostLike>> GetAllUserPostLikes(int userId)
        {
            return await _dao
                .Where(x=>x.UserId==userId)
                .ToListAsync();
        }

        public async Task<List<PostLike>> GetPostLikes(int postId)
        {
            return await _dao
                .Where(x=>x.PostId==postId)
                .ToListAsync();
        }

        public async Task<PostLike?> GetUserPostLike(int userId, int postId)
        {
            return await _dao
                .Where(x => x.UserId == userId && x.PostId == postId)
                .SingleOrDefaultAsync();
        }

        public ISqlFluentRepository<PostLike> Query()
        {
            return _sqlFluentRepository;
        }

        public async Task UpdatePostLike(PostLike postLike)
        {
            await _dao.UpdateAsync(postLike);
        }
    }
}
