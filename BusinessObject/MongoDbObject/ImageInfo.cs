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
}
