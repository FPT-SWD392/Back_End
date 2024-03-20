using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.MongoDbObject
{
    public class CommissionImageInfo : IMongoDbObject
    {
        [BsonId]
        public string ImageId = Guid.NewGuid().ToString();
        public required UploadedFileInfo Preview { get; set; }
        public required UploadedFileInfo Original { get; set; }
    }
}
