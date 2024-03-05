using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.NoSqlObject
{
    public class PostContent :IMongoDbObject
    {
        [BsonId]
        public int PostId { get; set; }
        public required string Content { get; set; }
    }
}
