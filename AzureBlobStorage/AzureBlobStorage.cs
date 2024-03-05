using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public Task<bool> DeleteFileAsync(string blobName)
        {
            throw new NotImplementedException();
        }

        public Task<Stream> DownloadFileAsync(string blobName)
        {
            throw new NotImplementedException();
        }

        public async Task<string> UploadFileAsync(MemoryStream file)
        {
            string blobName = Guid.NewGuid().ToString();
            if (file.Length > FILE_SIZE_LIMIT)
            {
                throw new Exception("File too big");
            }
            BlobClient blobClient = _containerClient.GetBlobClient(blobName);
            await blobClient.UploadAsync(file);
            file.Close();
            return blobName;
        }
    }
}
