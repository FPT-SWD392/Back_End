using BusinessObject.SqlObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public ISqlFluentRepository<ArtRating> Query();
    }
}
