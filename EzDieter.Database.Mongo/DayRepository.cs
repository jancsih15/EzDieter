using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EzDieter.Domain;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace EzDieter.Database.Mongo
{
    public class DayRepository : IDayRepository
    {
        private readonly IMongoCollection<Day> _days;

        public DayRepository(IMongoClient client, IConfiguration configuration)
        {
            _days = client.GetDatabase(configuration["database-name"]).GetCollection<Day>(configuration["days"]);
        }

        public async Task<IEnumerable<Day>> GetAll(Guid userId)
        {
            var result = await _days.FindAsync(x => x.UserId == userId);
            return await result.ToListAsync();
        }

        public async Task<Day> GetByDate(Guid userId, DateTime date)
        {
            var result = await _days.FindAsync(x => x.UserId == userId && x.Date == date);
            return result.SingleOrDefault();
        }

        public async Task<bool> GetById(Guid id)
        {
            var result = await _days.FindAsync(x => x.Id == id);
            return await result.AnyAsync();
        }

        public async Task Add(Day day)
        {
            await _days.InsertOneAsync(day);
        }

        public async Task Update(Day day)
        {
            await _days.ReplaceOneAsync(x => x.Id == day.Id, day);
        }

        public async Task Delete(Guid id)
        {
            await _days.FindOneAndDeleteAsync(x => x.Id == id);
        }
    }
}