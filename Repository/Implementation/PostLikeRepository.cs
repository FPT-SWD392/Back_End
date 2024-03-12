using BusinessObject.SqlObject;
using DataAccessObject;
using Repository.Interface;

namespace Repository.Implementation
{
    public class PostLikeRepository : IPostLikeRepository
    {
        private readonly IGenericDao<PostLike> _dao;
        public PostLikeRepository(IDaoFactory daoFactory)
        {
            _dao = daoFactory.CreateDao<PostLike>();
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

        public async Task UpdatePostLike(PostLike postLike)
        {
            await _dao.UpdateAsync(postLike);
        }
    }
}
