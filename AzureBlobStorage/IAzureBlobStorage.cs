namespace AzureBlobStorage
{
    public interface IAzureBlobStorage
    {
        Task<string> UploadFileAsync(byte[] file);
        Task<Stream> DownloadFileAsync(string blobName);
        Task<bool> DeleteFileAsync(string blobName);
    }
}
