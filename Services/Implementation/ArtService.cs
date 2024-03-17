using BusinessObject;
using BusinessObject.MongoDbObject;
using BusinessObject.SqlObject;
using DataAccessObject;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementation
{
    public class ArtService : IArtService
    {
        private readonly IArtInfoRepository _artInfoRepository;
        private readonly IGenericDao<Tag> _tagDao;
        private readonly IGenericDao<ImageInfo> _imageInfoDao;
        public ArtService(IArtInfoRepository artInfoRepository)
        {
            _artInfoRepository = artInfoRepository;
        }

        public async Task<List<ArtInfo>> GetArtList(string? searchValue, List<int> tagIds, int pageNumber)
        {
            return await _artInfoRepository.GetArtList(searchValue, tagIds, pageNumber);
        }
    }
}
