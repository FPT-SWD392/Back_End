
namespace Services
{
    public interface IArtRatingService
    {
        public Task<bool> RatingArtwork(int userId, int artId, int rating);
    }
}
