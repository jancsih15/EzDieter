using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EzDieter.Domain;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace EzDieter.Database.Mongo
{
    public class IngredientRepository : IIngredientRepository
    {
        private readonly IMongoCollection<Ingredient?> _ingredients;

        public IngredientRepository(IMongoClient client, IConfiguration configuration)
        {
            _ingredients = client.GetDatabase(configuration["database-name"]).GetCollection<Ingredient>(configuration["ingredients"]);
        }
        
        public async Task<IEnumerable<Ingredient?>> GetAll()
        {
            return await _ingredients.AsQueryable().ToListAsync();
        }

        // TODO is async needed at list? or any good?
        public async Task<IEnumerable<Ingredient?>> GetByName(string name)
        {
            var result = await _ingredients.FindAsync(x => x.Name == name);
            return await result.ToListAsync(); 
        }

        public async Task<Ingredient?> GetById(Guid id)
        {
            var result = await _ingredients.FindAsync(x => x.Id == id);
            
            return result.SingleOrDefault(); 
            // var result = await _ingredients
            //     .FindAsync(x => x.Id == id);
            // var any = await result.AnyAsync();
            // if (any)
            // {
            //     return result.SingleOrDefault();
            // }
            //
            // return null;
        }

        public async Task Add(Ingredient? ingredient)
        {
            await _ingredients.InsertOneAsync(ingredient);
        }

        public async Task Update(Ingredient? ingredient)
        {
            await _ingredients.ReplaceOneAsync(x => x.Id == ingredient.Id, ingredient);
        }

        public async Task Delete(Guid id)
        {
            await _ingredients.FindOneAndDeleteAsync(x => x.Id == id);
        }
    }
}