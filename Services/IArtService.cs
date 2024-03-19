using BusinessObject.DTO;

namespace Services
{
    public interface IArtService
    {
        public Task<ArtworkListDTO> GetArtList(string? searchValue, List<int> tagIds, int pageNumber);
        public Task<ArtworkListDTO> GetArtListForLoggedUser(int userId, string? searchValue, List<int> tagIds, int pageNumber);
        public Task<ArtworkListDTO> GetPurchasedArtList(int userId, string? searchValue, List<int> tagIds, int pageNumber);
        public Task<ArtworkListDTO> GetCreatedArtList(int creatorId, string? searchValue, List<int> tagIds, int pageNumber);
        public Task<ArtworkDetailDTO?> GetArtDetails(int artId);
        public Task<ArtworkDetailDTO?> GetArtDetails(int artId, int creatorId);
        public Task CreateArt(int creatorId, CreateArtRequest request);
        public Task<ImageDTO?> DownloadPreview(int artId);
        public Task<ImageDTO?> DownloadOriginal(int userId, int artId);
    }
}
