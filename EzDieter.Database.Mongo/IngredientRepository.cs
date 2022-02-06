using System.Collections.Generic;
using System.Threading.Tasks;
using EzDieter.Domain;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace EzDieter.Database.Mongo
{
    public class IngredientRepository : IIngredientRepository
    {
        private readonly IMongoClient _client;
        private readonly IConfiguration _configuration;
        private IMongoCollection<Ingredient> _ingredients;

        public IngredientRepository(IMongoClient client, IConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
            _ingredients = client.GetDatabase(configuration["database-name"]).GetCollection<Ingredient>(configuration["ingredients"]);
        }
        
        public Task<IEnumerable<Ingredient>> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Task<Ingredient> GetById(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task Add(Ingredient ingredient)
        {
            throw new System.NotImplementedException();
        }

        public Task Update(Ingredient ingredient)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}