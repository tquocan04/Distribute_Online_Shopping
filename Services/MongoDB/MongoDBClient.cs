using DTOs.MongoDb.Setting;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Services.MongoDB
{
    public class MongoDBClient
    {
        private readonly IMongoDatabase _database;

        public MongoDBClient(IOptions<MongoDBSetting> options)
        {
            var client = new MongoClient(options.Value.DbConnection);
            _database = client.GetDatabase(options.Value.DbName);
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return _database.GetCollection<T>(collectionName);
        }
    }
}
