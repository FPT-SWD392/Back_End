using BusinessObject.SqlObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IPostService
    {
        Task<Post?> GetPostById(int postId);
        Task<List<Post>?> GetAllPosts();
        Task CreatePost(Post post);
        Task UpdatePost(Post updatedPost);
        Task DeletePost(int postId);
    }
}
