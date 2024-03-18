using BusinessObject.SqlObject;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementation
{
    public class ArtRatingService : IArtRatingService
    {
        private readonly IUserInfoRepository _userInfoRepository;
        private readonly IArtRatingRepository _artRatingRepository;
        public ArtRatingService(IUserInfoRepository userInfoRepository, IArtRatingRepository artRatingRepository)
        {
            _userInfoRepository = userInfoRepository;
            _artRatingRepository = artRatingRepository;
        }

        public async Task<bool> RatingArtwork(int userId, int artId, int rating)
        {
            var e = await _artRatingRepository.GetUserArtRating(userId, artId);
            //check if user has rated before, if yes: update (??? not sure)
            if (e != null)
            {
                e.Rating = rating;
                await _artRatingRepository.UpdateArtRating(e);
                return true;
            }
            else
            {
                var ratingToBeCreated = new ArtRating()
                {
                    UserId = userId,
                    ArtId = artId,
                    Rating = rating,
                    RatingDate = DateTime.UtcNow,
                };
                await _artRatingRepository.CreateNewArtRating(ratingToBeCreated);
                return true;
            }
            

        }
    }
}
