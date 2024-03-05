using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureBlobStorage
{
    public interface IAzureBlobStorage
    {
            Task<string> UploadFileAsync(MemoryStream file);
            Task<Stream> DownloadFileAsync(string blobName);
            Task<bool> DeleteFileAsync(string blobName);
    }
}
