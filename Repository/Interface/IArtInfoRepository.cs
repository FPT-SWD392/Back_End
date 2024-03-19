using BusinessObject.DTO;
using BusinessObject.SqlObject;

namespace Repository.Interface
{
    public interface IArtInfoRepository
    {
        public Task CreateNewArt(ArtInfo artInfo);
        public Task DeleteArt(ArtInfo artInfo);
        public Task UpdateArt(ArtInfo artInfo);
        public Task<ArtInfo?> GetArtById(int id);
        public Task<ArtInfo?> GetCreatedArtByArtIdAndUserId(int userId,int artId);
        public Task<ArtworkDetailDTO?> GetArtDetails(int id);
        public Task<ArtworkDetailDTO?> GetArtDetails(int id, int creatorId);
        public Task<List<ArtInfo>> GetAllArts();
        public Task<ArtworkListDTO> GetArtList(string? searchValue, List<int> tagIds, int page);
        public Task<ArtworkListDTO> GetArtListForLoggedUser(int userId, string? searchValue, List<int> tagIds, int page);
        public Task<ArtworkListDTO> GetPurchasedArtList(int userId, string? searchValue, List<int> tagIds, int page);
        public Task<ArtworkListDTO> GetCreatedArtList(int creatorId, string? searchValue, List<int> tagIds, int page);
    }
}
