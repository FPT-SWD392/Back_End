using BusinessObject.DTO;

namespace Services
{
    public interface IArtService
    {
        public Task<List<ArtworkPreviewDTO>> GetArtList(string? searchValue, List<int> tagIds, int pageNumber);
        public Task CreateArt(int creatorId, CreateArtRequest request);
        public Task<ImageDTO?> DownloadPreview(int artId);
        public Task<ImageDTO?> DownloadOriginal(int userId, int artId);
    }
}
