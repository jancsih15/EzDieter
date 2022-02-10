using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EzDieter.Domain;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace EzDieter.Database.Mongo
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _users;

        public UserRepository(IMongoClient client, IConfiguration configuration)
        {
            _users = client.GetDatabase(configuration["database-name"]).GetCollection<User>(configuration["users"]);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _users.AsQueryable().ToListAsync();
        }

        public async Task<User> GetById(Guid? id)
        {
            var filter = Builders<User>.Filter.Eq(x => x.Id, id);
            var result = await _users.FindAsync(filter).Result.FirstAsync();
            return result;
        }

        public async Task Add(User user)
        {
            await _users.InsertOneAsync(user);
        }

        public async Task Update(User user)
        {
            await _users.ReplaceOneAsync(Builders<User>.Filter.Eq(x => x.Id, user.Id),user);
        }

        public async Task Delete(Guid id)
        {
            await _users.FindOneAndDeleteAsync(Builders<User>.Filter.Eq(x => x.Id, id));
        }
    }
}