using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace DataAccessObject
{
    internal class MongoDbGenericDao<T> : IGenericDao<T> where T : class
    {
        internal readonly IMongoCollection<T> _collection;
        internal readonly string _databaseName;
        internal readonly string _idFieldName;
        internal readonly IConfiguration _configuration;
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
        public async Task<IEnumerable<T>?> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }
        public async Task<long> CountAllAsync()
        {
            return await _collection.CountDocumentsAsync(_ => true);
        }
        public async Task<IEnumerable<T>?> GetByFilterAsync(Expression<Func<T, bool>> predicate)
        {
            return await _collection.Find(predicate).ToListAsync();
        }
        public async Task<long> CountByFilterAsync(Expression<Func<T, bool>> predicate)
        {
            return await _collection.CountDocumentsAsync(predicate);
        }
        public async Task<IEnumerable<T>?> GetByFilterPagedAsync(
            int pageNumber, 
            int pageSize, 
            bool isAscending, 
            Expression<Func<T, object>> propertySelector,
            Expression<Func<T, bool>> predicate)
        {
            if (pageNumber < 1) throw new Exception("Invalid page number");
            if (pageSize < 1) throw new Exception("Invalid page size");
            int firstPageElementNumber = (pageNumber - 1) * pageSize;
            if (isAscending)
            {
                return await _collection
                .Find(predicate)
                .Skip(firstPageElementNumber)
                .Limit(pageSize)
                .SortBy(propertySelector)
                .ToListAsync();
            }
            return await _collection
                .Find(predicate)
                .Skip (firstPageElementNumber)
                .Limit(pageSize)
                .SortByDescending(propertySelector)
                .ToListAsync();
            
        }
        public async Task AddAsync(T item)
        {
            await _collection.InsertOneAsync(item);
        }

        public async Task DeleteAsync(T item)
        {
            var filter = Builders<T>.Filter.Eq(_idFieldName, item.ToBsonDocument()[_idFieldName]);
            DeleteResult deletedResult = await _collection.DeleteOneAsync(filter);
            if (deletedResult.DeletedCount !=1)
            {
                throw new Exception($"Delete failed. Number of deleted record(s):{deletedResult.DeletedCount}");
            }
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
    }
}
