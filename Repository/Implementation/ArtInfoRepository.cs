using AutoMapper;
using BusinessObject;
using BusinessObject.DTO;
using BusinessObject.SqlObject;
using DataAccessObject;
using Microsoft.Extensions.Configuration;
using Repository.Interface;
using System.Linq.Expressions;

namespace Repository.Implementation
{
    public class ArtInfoRepository : IArtInfoRepository
    {
        private readonly IGenericDao<ArtInfo> _artInfoDao;
        private readonly IMapper _mapper;
        private readonly int pageSize;
        public ArtInfoRepository(IDaoFactory daoFactory, IConfiguration configuration, IMapper mapper)
        {
            _mapper = mapper;
            _artInfoDao = daoFactory.CreateDao<ArtInfo>();
            if (false == int.TryParse(configuration["AppSetting:ItemPerPage"], out pageSize))
            {
                pageSize = 10;
            }
        }

        public async Task CreateNewArt(ArtInfo artInfo)
        {
            await _artInfoDao.CreateAsync(artInfo);
        }

        public async Task DeleteArt(ArtInfo artInfo)
        {
            await _artInfoDao.DeleteAsync(artInfo);
        }

        public async Task<List<ArtInfo>> GetAllArts()
        {
            return await _artInfoDao
                .Query()
                .ToListAsync();
        }

        public async Task<ArtInfo?> GetArtById(int id)
        {
            return await _artInfoDao
                .Query()
                .Where(x => x.ArtId == id).SingleOrDefaultAsync();
        }

        

        public async Task<List<ArtworkPreviewDTO>> GetArtList(string? searchValue, List<int> tagIds, int page)
        {
            int skip = (page - 1) * pageSize;
            Expression<Func<ArtInfo, bool>> expression = (ArtInfo) => ArtInfo.ArtName.Contains(searchValue ?? "");
            expression = And(expression, (ArtInfo) => ArtInfo.Status != ArtStatus.Unavailable);
            foreach (var tagId in tagIds)
            {
                expression = And(expression, (ArtInfo) => ArtInfo.ArtTags.Select(x => x.TagId).Contains(tagId));
            }
            List<ArtInfo> artList = await _artInfoDao
                .Query()
                .Include(x => x.ArtTags)
                .Include(x => x.ArtRatings)
                .Include(x => x.CreatorInfo.UserInfo)
                .Where(expression)
                .OrderByDescending(artInfo => artInfo.CreatedDate)
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();
            List<ArtworkPreviewDTO> result = (List<ArtworkPreviewDTO>)_mapper.Map(artList,typeof(List<ArtInfo>),typeof(List<ArtworkPreviewDTO>));
            return result;
        }

        private static Expression<Func<T, bool>> And<T>(Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
        {
            var param = Expression.Parameter(typeof(T));
            var andExpression = Expression.AndAlso(Expression.Invoke(left, param), Expression.Invoke(right, param));

            return Expression.Lambda<Func<T, bool>>(andExpression, param);
        }

        public async Task UpdateArt(ArtInfo artInfo)
        {
            await _artInfoDao.UpdateAsync(artInfo);
        }
    }
}
