using BusinessObject.SqlObject;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.NoSqlObject
{
    public class ImageTags : IMongoDbObject
    {
        [BsonId]
        public int ImageId { get; set; }
        public required List<string> Tags { get; set; }
    }
}
