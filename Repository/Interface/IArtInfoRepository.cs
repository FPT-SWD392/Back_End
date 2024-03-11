using BusinessObject.SqlObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IArtInfoRepository
    {
        public Task CreateNewArt(ArtInfo artInfo);
        public Task DeleteArt(ArtInfo artInfo);
        public Task UpdateArt(ArtInfo artInfo);
        public Task<ArtInfo?> GetArtById(int id);
        public Task<List<ArtInfo>> GetAllArts();
    }
}
