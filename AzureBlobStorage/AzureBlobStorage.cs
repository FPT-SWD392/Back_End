using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;

namespace AzureBlobStorage
{
    public class AzureBlobStorage : IAzureBlobStorage
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly BlobContainerClient _containerClient;

        private const int ONE_MEGABYTE = 1024 * 1024;
        private const int FILE_SIZE_LIMIT = 10 * ONE_MEGABYTE;

        public AzureBlobStorage(IConfiguration configuration)
        {
            string connectionString = configuration
                .GetConnectionString("AzureBlobStorageConnectionString")
                ?? throw new Exception("Failed to get connection string");
            string containerName = configuration["AzureBlob:ContainerName"]
                ?? throw new Exception("Failed to get container name");
            _blobServiceClient = new BlobServiceClient(connectionString);
            _containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            _containerClient.CreateIfNotExists();
        }
        public async Task<bool> DeleteFileAsync(string blobName)
        {
            BlobClient blobClient = _containerClient.GetBlobClient(blobName);
            return await blobClient.DeleteIfExistsAsync();
        }

        public async Task<Stream> DownloadFileAsync(string blobName)
        {
            BlobClient blobClient = _containerClient.GetBlobClient(blobName);
            return await blobClient.OpenReadAsync();
        }

        public async Task<string> UploadFileAsync(byte[] file)
        {
            string blobName = Guid.NewGuid().ToString();
            if (file.Length > FILE_SIZE_LIMIT)
            {
                throw new Exception("File too big");
            }
            using MemoryStream stream = new(file);
            BlobClient blobClient = _containerClient.GetBlobClient(blobName);
            await blobClient.UploadAsync(stream);
            return blobName;
        }
    }
}
