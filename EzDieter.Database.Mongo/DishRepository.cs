using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EzDieter.Domain;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace EzDieter.Database.Mongo
{
    public class DishRepository : IDishRepository
    {
        private readonly IMongoCollection<Dish> _dishes;

        public DishRepository(IMongoClient client, IConfiguration configuration)
        {
            _dishes = client.GetDatabase(configuration["database-name"]).GetCollection<Dish>(configuration["dishes"]);
        }


        public async Task<IEnumerable<Dish>> GetAll()
        {
            return await _dishes.AsQueryable().ToListAsync();
        }

        public async Task<Dish> GetById(Guid id)
        {
            var result = await _dishes.FindAsync(x => x.Id == id);
            
            return result.SingleOrDefault();
        }

        public async Task<List<Dish>> GetByName(string name)
        {
            var result = await _dishes.FindAsync(x => x.Name == name);
            return await result.ToListAsync();
        }

        public async Task Add(Dish dish)
        {
            await _dishes.InsertOneAsync(dish);
        }

        public async Task Update(Dish dish)
        {
            await _dishes.ReplaceOneAsync(x => x.Id == dish.Id, dish);
        }

        public async Task Delete(Guid id)
        {
            await _dishes.FindOneAndDeleteAsync(x => x.Id == id);
        }
    }
}