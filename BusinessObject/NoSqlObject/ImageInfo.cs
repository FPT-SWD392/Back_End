using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.NoSqlObject
{
    public class ImageInfo : IMongoDbObject
    {
        [BsonId]
        public int Id { get; set; }
        public required UploadedFileInfo Preview { get; set; }
        public required UploadedFileInfo Original { get; set; }
    }
    public class UploadedFileInfo
    {
        public required string BlobName { get; set; }
        public required string SHA256 { get; set; }
        public required string FileSize { get; set; }
        public required ImageType ImageType { get; set; }
        public long Size { get; set; }
    }
}
