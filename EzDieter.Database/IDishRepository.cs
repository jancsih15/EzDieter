using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EzDieter.Domain;

namespace EzDieter.Database
{
    public interface IDishRepository
    {
        Task<IEnumerable<Dish>> GetAll();
        Task<Dish> GetById(Guid id);
        Task<List<Dish>> GetByName(string name);
      
        Task Add(Dish dish);
        Task Update(Dish dish);
        Task Delete(Guid id);
    }
}