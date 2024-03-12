using MongoDB.Bson.Serialization.Attributes;

namespace BusinessObject.MongoDbObject
{
    public class ImageInfo : IMongoDbObject
    {
        [BsonId]
        public int ImageId { get; set; }
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
