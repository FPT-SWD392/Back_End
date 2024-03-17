using BusinessObject.SqlObject;
using MongoDB.Driver;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementation
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserInfoRepository _userInfoRepository;

        public PostService(IPostRepository postRepository, IUserInfoRepository userInfoRepository)
        {
            _postRepository = postRepository;
            _userInfoRepository = userInfoRepository;
        }

        public Task CreatePost(Post post)
        {
            return _postRepository.CreateNewPost(post);
        }

        public async Task DeletePost(int postId)
        {
            var post = _postRepository.GetPostById(postId);
            await _postRepository.DeletePost(post.Result);
        }

        public async Task<List<Post>> GetAllPosts()
        {
            return await _postRepository.GetAllPosts();
        }

        public async Task<Post> GetPostById(int postId)
        {
            return await _postRepository.GetPostById(postId);
        }

        public async Task UpdatePost(Post updatedPost)
        {
            await _postRepository.UpdatePost(updatedPost);
        }
    }
}
