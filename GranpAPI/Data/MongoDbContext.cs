using MongoDB.Driver;
using Granp.Models.Types;
using Microsoft.Extensions.Options;

namespace Granp.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database = null!;

        public MongoDbContext(IOptions<DatabaseSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _database.GetCollection<T>(name);
        }
    }
}