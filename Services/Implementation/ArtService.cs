using AzureBlobStorage;
using BusinessObject;
using BusinessObject.DTO;
using BusinessObject.MongoDbObject;
using BusinessObject.SqlObject;
using Microsoft.Extensions.Configuration;
using Repository.Interface;
using Utils.ImageUtil;

namespace Services.Implementation
{
    public class ArtService : IArtService
    {
        private readonly IArtInfoRepository _artInfoRepository;
        private readonly ICreatorInfoRepository _creatorInfoRepository;
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IImageUtil _imageUtil;
        private readonly IAzureBlobStorage _azureBlobStorage;
        private readonly IImageInfoRepository _imageInfoRepository;
        private readonly int _artUploadLimit;
        public ArtService(
            IArtInfoRepository artInfoRepository,
            ICreatorInfoRepository creatorInfoRepository,
            IAzureBlobStorage azureBlobStorage,
            IImageInfoRepository imageInfoRepository,
            IPurchaseRepository purchaseRepository,
            IImageUtil imageUtil,
            IConfiguration configuration)
        {
            _artInfoRepository = artInfoRepository;
            _creatorInfoRepository = creatorInfoRepository;
            _imageInfoRepository = imageInfoRepository;
            _purchaseRepository = purchaseRepository;
            _azureBlobStorage = azureBlobStorage;
            _imageUtil = imageUtil;
            if (false == int.TryParse(configuration["AppSetting:ArtLimit"],out _artUploadLimit))
            {
                _artUploadLimit = int.MaxValue;
            }
        }

        public async Task CreateArt(int creatorId, CreateArtRequest request)
        {
            Task<ArtInfo> artInfoTask = CreateArtInfo(creatorId, request);

            using MemoryStream memoryStream = new();
            request.ImageFile.OpenReadStream().CopyTo(memoryStream);
            byte[] originalImage = memoryStream.ToArray();
            Task<string> uploadOrigial = _azureBlobStorage.UploadFileAsync(originalImage);
            Task<(string blobName,int imageSize)> uploadPreview = ProcessAndUploadPreivew(originalImage);

            await Task.WhenAll(uploadOrigial, uploadPreview,artInfoTask);

            ImageInfo imageInfo = new()
            {
                ArtId = (await artInfoTask).ArtId,
                Original = new() 
                { 
                    BlobName = await uploadOrigial,
                    FileSize = originalImage.Length, 
                    ImageType = request.ImageFile.ContentType,
                    FileName = request.ImageFile.FileName
                },
                Preview = new() 
                { 
                    BlobName = (await uploadPreview).blobName,
                    FileSize = (await uploadPreview).imageSize, 
                    ImageType = request.ImageFile.ContentType,
                    FileName = $"preview_{request.ImageFile.FileName}"
                }
            };
            await _imageInfoRepository.CreateNewImageInfo(imageInfo);
        }

        private async Task<ArtInfo> CreateArtInfo(int creatorId, CreateArtRequest request)
        {
            CreatorInfo creatorInfo = await _creatorInfoRepository.GetCreatorInfo(creatorId) ??
                throw new Exception("Invalid creator id");
            ArtInfo artinfo = new()
            {
                ArtName = request.Name,
                CreatedDate = DateTime.Now,
                CreatorId = creatorInfo.CreatorId,
                Description = request.Description,
                Status = request.ArtStatus,
                Price = request.Price,
                UpdateDate = DateTime.Now,
            };
            await _artInfoRepository.CreateNewArt(artinfo);
            return artinfo;
        }

        private async Task<(string blobName, int imageSize)> ProcessAndUploadPreivew(byte[] originalImage)
        {
            byte[] previewImage = _imageUtil
                .SetImage(originalImage)
                .Resize()
                .AddWatermark()
                .GetResult();
            return (await _azureBlobStorage.UploadFileAsync(previewImage), previewImage.Length);
        }

        public async Task<ImageDTO?> DownloadPreview(int artId)
        {
            ImageInfo? imageInfo = await _imageInfoRepository.GetImageInfoById(artId);
            if (imageInfo == null) return null;
            Stream stream = await _azureBlobStorage.DownloadFileAsync(imageInfo.Preview.BlobName);
            return new ImageDTO() { FileName = imageInfo.Preview.FileName,FileStream = stream,ImageType = imageInfo.Preview.ImageType};
        }
        public async Task<ImageDTO?> DownloadOriginal(int userId,int artId)
        {
            Purchase? purchase = await _purchaseRepository.GetPurchaseByUserIdAndArtId(userId, artId);
            ArtInfo? artistCreatedArtInfo = await _artInfoRepository.GetCreatedArtByArtIdAndUserId(userId, artId);
            if (purchase == null && artistCreatedArtInfo == null) return null;
            ImageInfo? imageInfo = await _imageInfoRepository.GetImageInfoById(artId);
            if (imageInfo == null) return null;

            Stream stream = await _azureBlobStorage.DownloadFileAsync(imageInfo.Original.BlobName);
            return new ImageDTO() { FileName = imageInfo.Original.FileName, FileStream = stream, ImageType = imageInfo.Original.ImageType };
        }
        public async Task<ArtworkListDTO> GetArtList(string? searchValue, List<int> tagIds, int pageNumber)
        {
            return await _artInfoRepository.GetArtList(searchValue, tagIds, pageNumber);
        }

        public async Task<ArtworkListDTO> GetArtListForLoggedUser(int userId, string? searchValue, List<int> tagIds, int pageNumber)
        {
            return await _artInfoRepository.GetArtListForLoggedUser(userId, searchValue, tagIds, pageNumber);
        }

        public async Task<ArtworkDetailDTO?> GetArtDetails(int artId)
        {
            ArtworkDetailDTO? artwork = await _artInfoRepository.GetArtDetails(artId);
            if (artwork == null || artwork.Status == ArtStatus.Unavailable) return null;
            return artwork;
        }
        public async Task<ArtworkDetailDTO?> GetArtDetails(int artId, int creatorId)
        {
            return await _artInfoRepository.GetArtDetails(artId,creatorId);
        }

        public async Task<ArtworkListDTO> GetPurchasedArtList(int userId, string? searchValue, List<int> tagIds, int pageNumber)
        {
            return await _artInfoRepository.GetPurchasedArtList(userId, searchValue, tagIds, pageNumber);
        }

        public async Task<ArtworkListDTO> GetCreatedArtList(int creatorId, string? searchValue, List<int> tagIds, int pageNumber)
        {
            return await _artInfoRepository.GetCreatedArtList(creatorId, searchValue, tagIds, pageNumber);
        }
    }
}
