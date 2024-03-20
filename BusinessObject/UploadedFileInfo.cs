using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class UploadedFileInfo
    {
        public required string BlobName { get; set; }
        public required long FileSize { get; set; }
        public required string ImageType { get; set; }
        public required string FileName { get; set; }
    }
}
