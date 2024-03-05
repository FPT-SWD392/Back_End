using BusinessObject.SqlObject;
using DataAccessObject;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementation
{
    public class ArtRatingRepository : IArtRatingRepository
    {
        public readonly IGenericDao<ArtRating> _artRatingDao;
        private readonly ISqlFluentRepository<ArtRating> _sqlFluentRepository;
        public ArtRatingRepository(
            IDaoFactory daoFactory,
            ISqlFluentRepository<ArtRating> sqlFluentRepository)
        {
            _artRatingDao = daoFactory.CreateDao<ArtRating>();
            _sqlFluentRepository = sqlFluentRepository;
        }
        public async Task CreateNewArtRating(ArtRating artRating)
        {
            await _artRatingDao.CreateAsync(artRating);
        }

        public async Task DeleteArtRating(ArtRating artRating)
        {
            await _artRatingDao.DeleteAsync(artRating);
        }

        public async Task UpdateArtRating(ArtRating artRating)
        {
            await _artRatingDao.UpdateAsync(artRating);
        }

        public async Task<List<ArtRating>> GetArtRatings(int artId)
        {
            return await _artRatingDao
                .Where(a=>a.ArtId == artId)
                .ToListAsync();
        }

        public ISqlFluentRepository<ArtRating> Query()
        {
            return _sqlFluentRepository;
        }

        public async Task<List<ArtRating>> GetAllUserArtRatings(int userId)
        {
            return await _artRatingDao
                .Where(a => a.UserId == userId)
                .ToListAsync();
        }

        public async Task<ArtRating?> GetUserArtRating(int userId, int artId)
        {
            return await _artRatingDao
                .Where(a=>a.UserId == userId && a.ArtId == artId)
                .SingleOrDefaultAsync();
        }
    }
}
