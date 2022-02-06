using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace EzDieter.Database.Mongo
{
    public class MongoClientFactory
    {
        private readonly IConfiguration _configuration;

        public MongoClientFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IMongoClient Create()
        {
            return new MongoClient(_configuration["connectionString"]);
        }
    }
}