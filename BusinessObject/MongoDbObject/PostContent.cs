using MongoDB.Bson.Serialization.Attributes;

namespace BusinessObject.MongoDbObject
{
    public class PostContent :IMongoDbObject
    {
        [BsonId]
        public int PostId { get; set; }
        public required string Content { get; set; }
    }
}
