using BusinessObject;
using BusinessObject.MongoDbObject;
using BusinessObject.SqlObject;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace DataAccessObject
{
    public class DaoFactory : IDaoFactory
    {
        private readonly IMongoClient _mongoClient;
        private readonly SqlDbContext _sqlDbContext;
        private readonly IConfiguration _configuration;
        public DaoFactory(IMongoClient mongoClient, SqlDbContext sqlDbContext, IConfiguration configuration)
        {
            _mongoClient = mongoClient;
            _sqlDbContext = sqlDbContext;
            _configuration = configuration;
        }
        public IGenericDao<T> CreateDao<T>() where T : class 
        {
            if (typeof(IMongoDbObject).IsAssignableFrom(typeof(T)))
            {
                return new MongoDbGenericDao<T>(_mongoClient,_configuration);
            }
            if (typeof(ISqlObject).IsAssignableFrom(typeof(T)))
            {
                return new SqlGenericDao<T>(_sqlDbContext);
            }
            throw new InvalidOperationException($"Unable to determine the Dao for the type {nameof(T)}");
        }
    }
}
