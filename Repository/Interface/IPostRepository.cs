using BusinessObject.SqlObject;
namespace Repository.Interface
{
    public interface IPostRepository
    {
        public Task CreateNewPost(Post post);
        public Task DeletePost(Post post);
        public Task UpdatePost(Post post);
        public Task<Post?> GetPostById(int postId);
        public Task<List<Post>> GetAllArtistPosts(int creatorId);
    }
}
