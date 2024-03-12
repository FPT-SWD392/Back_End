using BusinessObject.SqlObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IPostLikeRepository
    {
        public Task CreateNewPostLike(PostLike postLike);
        public Task DeletePostLike(PostLike postLike);
        public Task UpdatePostLike(PostLike postLike);
        public Task<List<PostLike>> GetAllUserPostLikes(int userId);
        public Task<List<PostLike>> GetPostLikes(int postId);
        public Task<PostLike?> GetUserPostLike(int userId, int postId);
        public Task<long> CountPostLikes(int postId);
    }
}
