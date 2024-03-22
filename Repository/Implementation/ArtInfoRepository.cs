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
        private readonly IGenericDao<UserInfo> _userInfoDao;
        private readonly IGenericDao<CreatorInfo> _creatorInfoDao;
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
            _userInfoDao = daoFactory.CreateDao<UserInfo>();
            _creatorInfoDao = daoFactory.CreateDao<CreatorInfo>();
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
        public async Task<ArtworkListDTO> GetArtList(string? searchValue, List<int> tagIds, int page)
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
            List<ArtworkPreviewDTO> result = (List<ArtworkPreviewDTO>)_mapper.Map(artList, typeof(List<ArtInfo>), typeof(List<ArtworkPreviewDTO>));
            long total = await _artInfoDao
                .Query()
                .Where(expression)
                .CountAsync();
            ArtworkListDTO artworkListDTO = new()
            {
                ArtworkPreviews = result,
                CurrentPage = page,
                PageCount = (int)Math.Ceiling(total / (double)pageSize)
            };
            return artworkListDTO;
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

        public async Task<ArtworkListDTO> GetArtListForLoggedUser(int userId, string? searchValue, List<int> tagIds, int page)
        {
            int skip = (page - 1) * pageSize;

            UserInfo userInfo = await _userInfoDao
                .Query()
                .Include(x => x.CreatorInfo.ArtInfos)
                .Include(x => x.Purchases)
                .Where(x => x.UserId == userId)
                .SingleOrDefaultAsync() ?? throw new Exception("Invalid userId");

            List<int> purchasedArtIds = userInfo.Purchases.Select(x => x.ArtId).ToList();
            List<int> userCreatedArtIds = userInfo.CreatorInfo?.ArtInfos.Select(x => x.ArtId).ToList() ?? new();

            List<int> ignoredArtIds = purchasedArtIds.Concat(userCreatedArtIds).ToList();

            Expression<Func<ArtInfo, bool>> expression = ArtInfo =>
                ArtInfo.ArtName.Contains(searchValue ?? "") &&
                ArtInfo.Status != ArtStatus.Unavailable;

            expression = And(expression, ArtInfo => false == ignoredArtIds.Contains(ArtInfo.ArtId));
            foreach (var tagId in tagIds)
            {
                expression = And(expression, ArtInfo => ArtInfo.ArtTags.Select(x => x.TagId).Contains(tagId));
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
            List<ArtworkPreviewDTO> result = _mapper.Map<List<ArtworkPreviewDTO>>(artList);
            long total = await _artInfoDao
                .Query()
                .Where(expression)
                .CountAsync();
            ArtworkListDTO artworkListDTO = new()
            {
                ArtworkPreviews = result,
                CurrentPage = page,
                PageCount = (int)Math.Ceiling(total / (double)pageSize)
            };
            return artworkListDTO;
        }

        public async Task<ArtworkDetailDTO?> GetArtDetails(int id)
        {
            ArtInfo? art = await _artInfoDao
                .Query()
                .Include(x => x.ArtTags)
                .Include(x => x.CreatorInfo.UserInfo)
                .Include(x => x.ArtRatings)
                .Where(x => x.ArtId == id)
                .SingleOrDefaultAsync();
            return _mapper.Map<ArtworkDetailDTO>(art);
        }
        public async Task<ArtworkDetailDTO?> GetArtDetails(int id, int creatorId)
        {
            ArtInfo? art = await _artInfoDao
                .Query()
                .Include(x => x.ArtTags)
                .Include(x => x.CreatorInfo.UserInfo)
                .Include(x => x.ArtRatings)
                .Where(x => x.ArtId == id && x.CreatorId == creatorId)
                .SingleOrDefaultAsync();
            return _mapper.Map<ArtworkDetailDTO>(art);
        }

        public async Task<ArtworkListDTO> GetPurchasedArtList(int userId, string? searchValue, List<int> tagIds, int page)
        {
            int skip = (page - 1) * pageSize;

            UserInfo userInfo = await _userInfoDao
                .Query()
                .Include(x => x.Purchases)
                .Where(x => x.UserId == userId)
                .SingleOrDefaultAsync() ?? throw new Exception("Invalid userId");

            List<int> purchasedArtIds = userInfo.Purchases.Select(x => x.ArtId).ToList();

            Expression<Func<ArtInfo, bool>> expression = ArtInfo =>
                ArtInfo.ArtName.Contains(searchValue ?? "");

            expression = And(expression, ArtInfo => purchasedArtIds.Contains(ArtInfo.ArtId));
            foreach (var tagId in tagIds)
            {
                expression = And(expression, ArtInfo => ArtInfo.ArtTags.Select(x => x.TagId).Contains(tagId));
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
            List<ArtworkPreviewDTO> result = _mapper.Map<List<ArtworkPreviewDTO>>(artList);
            long total = await _artInfoDao
                .Query()
                .Where(expression)
                .CountAsync();
            ArtworkListDTO artworkListDTO = new()
            {
                ArtworkPreviews = result,
                CurrentPage = page,
                PageCount = (int)Math.Ceiling(total / (double)pageSize)
            };
            return artworkListDTO;
        }

        public async Task<ArtworkListDTO> GetCreatedArtList(int creatorId, string? searchValue, List<int> tagIds, int page)
        {
            int skip = (page - 1) * pageSize;

            CreatorInfo creatorInfo = await _creatorInfoDao
                .Query()
                .Include(x => x.ArtInfos)
                .Where(x => x.CreatorId == creatorId)
                .SingleOrDefaultAsync() ?? throw new Exception("Invalid creatorId");

            List<int> userCreatedArtIds = creatorInfo.ArtInfos.Select(x => x.ArtId).ToList();

            Expression<Func<ArtInfo, bool>> expression = ArtInfo =>
                ArtInfo.ArtName.Contains(searchValue ?? "");

            expression = And(expression, ArtInfo => userCreatedArtIds.Contains(ArtInfo.ArtId));
            foreach (var tagId in tagIds)
            {
                expression = And(expression, ArtInfo => ArtInfo.ArtTags.Select(x => x.TagId).Contains(tagId));
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
            List<ArtworkPreviewDTO> result = _mapper.Map<List<ArtworkPreviewDTO>>(artList);
            long total = await _artInfoDao
                .Query()
                .Where(expression)
                .CountAsync();
            ArtworkListDTO artworkListDTO = new()
            {
                ArtworkPreviews = result,
                CurrentPage = page,
                PageCount = (int)Math.Ceiling(total / (double)pageSize)
            };
            return artworkListDTO;
        }

        public async Task<ArtInfo?> GetCreatedArtByArtIdAndUserId(int userId,int artId)
        {
            ArtInfo? artInfo = await _artInfoDao
                .Query()
                .Include(x => x.CreatorInfo.UserInfo)
                .Where(x => x.ArtId == artId)
                .SingleOrDefaultAsync();
            if (artInfo == null || artInfo.CreatorInfo.UserInfo.UserId != userId) return null;
            return artInfo;
        }
    }
}
