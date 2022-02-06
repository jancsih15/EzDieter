using System.Collections.Generic;
using System.Threading.Tasks;
using EzDieter.Domain;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace EzDieter.Database.Mongo
{
    public class MealRepository : IMealRepository
    {
        private readonly IMongoClient _client;
        private readonly IConfiguration _configuration;
        private IMongoCollection<Meal> _mealsPublic;
        private IMongoCollection<Meal> _mealsPrivate;

        public MealRepository(IMongoClient client, IConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
            _mealsPublic = client.GetDatabase(configuration["database-name"]).GetCollection<Meal>(configuration["meals-public"]);
            _mealsPrivate = client.GetDatabase(configuration["database-name"]).GetCollection<Meal>(configuration["meals-private"]);
        }
        
        public Task<IEnumerable<Meal>> GetAllPublic()
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Meal>> GetAllPrivateOfUser(User user)
        {
            throw new System.NotImplementedException();
        }

        public Task<Meal> GetById(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Meal> GetPrivateById(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task Add(Meal meal)
        {
            throw new System.NotImplementedException();
        }

        public Task AddPrivate(Meal meal)
        {
            throw new System.NotImplementedException();
        }

        public Task Update(Meal meal)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdatePrivate(Meal meal)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task DeletePrivate(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}