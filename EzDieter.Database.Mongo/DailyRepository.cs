using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EzDieter.Domain;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace EzDieter.Database.Mongo
{
    public class DailyRepository : IDailyRepository
    {
        private readonly IMongoClient _client;
        private readonly IConfiguration _configuration;
        private IMongoCollection<Daily> _days;

        public DailyRepository(IMongoClient client, IConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
            _days = client.GetDatabase(configuration["database-name"]).GetCollection<Daily>(configuration["days"]);
        }
        
        public Task<IEnumerable<Daily>> GetByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<Daily> GetByDate(string userId, DateOnly date)
        {
            throw new NotImplementedException();
        }

        public Task Add(Daily day)
        {
            throw new NotImplementedException();
        }

        public Task Update(Daily day)
        {
            throw new NotImplementedException();
        }
    }
}