using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EzDieter.Domain;

namespace EzDieter.Database
{
    public interface IDayRepository
    {
        Task<IEnumerable<Day>> GetAll(Guid userId);
        Task<bool> GetById(Guid id);
        Task<Day> GetByDate(Guid userId, DateTime date);
        Task Add(Day day);
        Task Update(Day day);
        Task Delete(Guid id);
    }
}