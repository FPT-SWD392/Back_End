using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.MongoDbObject
{
    public class SystemRevenue : IMongoDbObject
    {
        [BsonId]
        public ObjectId Id { get;set; }
        public double Amount { get; set; }
        public string Description { get; set; }
        public DateTime Date {  get; set; }

    }
}
