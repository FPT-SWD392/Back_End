using BusinessObject.SqlObject;

namespace Repository.Interface
{
    public interface IArtRatingRepository
    {
        public Task CreateNewArtRating(ArtRating artRating);
        public Task DeleteArtRating(ArtRating artRating);
        public Task UpdateArtRating(ArtRating artRating);
        public Task<List<ArtRating>> GetAllUserArtRatings(int userId);
        public Task<List<ArtRating>> GetArtRatings(int artId);
        public Task<ArtRating?> GetUserArtRating(int userId, int artId);
    }
}
