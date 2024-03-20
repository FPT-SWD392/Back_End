using MongoDB.Bson.Serialization.Attributes;

namespace BusinessObject.MongoDbObject
{
    public class AdminAccount : IMongoDbObject
    {
        [BsonId]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string AdminName { get; set; }
        public DateTime CreatedDate { get; set; }
        public AdminAccountStatus Status { get; set; }
    }
}
