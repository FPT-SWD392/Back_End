using DataAccessObject.GenericQueryable;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq.Expressions;

namespace DataAccessObject
{
    public class MongoDbGenericDao<T> : IGenericDao<T> where T : class
    {
        private readonly IMongoCollection<T> _collection;
        private readonly string _databaseName;
        private readonly string _idFieldName;
        private readonly IConfiguration _configuration;
        public MongoDbGenericDao(IMongoClient mongoClient, IConfiguration configuration)
        {
            _configuration = configuration;
            _databaseName = _configuration["MongoDb:DatabaseName"] 
                ?? throw new Exception("Can not get MongoDb database name");
            _idFieldName = _configuration["MongoDb:IdFieldName"] 
                ?? throw new Exception("Can not get MongoDb default id field name");

            var database = mongoClient.GetDatabase(_databaseName);
            _collection = database.GetCollection<T>(typeof(T).Name);
        }
        public async Task CreateAsync(T item)
        {
            await _collection.InsertOneAsync(item);
        }
        public async Task UpdateAsync(T item)
        {

            var filter = Builders<T>.Filter.Eq(_idFieldName, item.ToBsonDocument()[_idFieldName]);
            var replaceOneResult = await _collection.ReplaceOneAsync(filter, item);
            if (replaceOneResult.MatchedCount != 1)
            {
                throw new Exception($"Update failed. Number of matched record(s):{replaceOneResult.MatchedCount}. " +
                    $"Number of modified record(s):{replaceOneResult.ModifiedCount}");
            }
        }
        public async Task DeleteAsync(T item)
        {
            var filter = Builders<T>.Filter.Eq(_idFieldName, item.ToBsonDocument()[_idFieldName]);
            DeleteResult deletedResult = await _collection.DeleteOneAsync(filter);
            if (deletedResult.DeletedCount != 1)
            {
                throw new Exception($"Delete failed. Number of deleted record(s):{deletedResult.DeletedCount}");
            }
        }

        public IGenericQueryable<T> Query()
        {
            return new MongoDbGenericQueryable<T>(_collection.AsQueryable());
        }
    }
}
