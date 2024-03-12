using BusinessObject.SqlObject;

namespace Repository.Interface
{
    public interface IFollowRepository
    {
        public Task CreateNewFollow(Follow follow);
        public Task DeleteFollow(Follow follow);
        public Task UpdateFollow(Follow follow);
        public Task<List<Follow>> GetAllUserFollows(int userId);
        public Task<List<Follow>> GetArtistFollowers(int creatorId);
        public Task<Follow?> GetFollow(int userId, int creatorId);
        public Task<long> CountArtistFollows(int creatorId);
    }
}
