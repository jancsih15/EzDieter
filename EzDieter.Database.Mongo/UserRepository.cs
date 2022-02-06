using System.Collections.Generic;
using System.Threading.Tasks;
using EzDieter.Domain;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace EzDieter.Database.Mongo
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoClient _client;
        private readonly IConfiguration _configuration;
        private IMongoCollection<User> _users;

        public UserRepository(IMongoClient client, IConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
            //_users = client.GetDatabase(configuration["database-name"]).GetCollection<User>(configuration["users"]);
            _users = client.GetDatabase("ez-dieter").GetCollection<User>("users");
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _users.AsQueryable().ToListAsync();
        }

        public Task<User> GetById(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task Add(User user)
        {
            throw new System.NotImplementedException();
        }

        public Task Update(User user)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}