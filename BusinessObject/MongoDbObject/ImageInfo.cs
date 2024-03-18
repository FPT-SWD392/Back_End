using MongoDB.Bson.Serialization.Attributes;

namespace BusinessObject.MongoDbObject
{
    public class ImageInfo : IMongoDbObject
    {
        [BsonId]
        public int ArtId { get; set; }
        public required UploadedFileInfo Preview { get; set; }
        public required UploadedFileInfo Original { get; set; }
    }
    public class UploadedFileInfo
    {
        public required string BlobName { get; set; }
        public required long FileSize { get; set; }
        public required string ImageType { get; set; }
        public required string FileName { get; set; }
    }
}
