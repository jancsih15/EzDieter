using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using EzDieter.Domain;

namespace EzDieter.Database
{
    public interface IIngredientRepository
    {
        Task<IEnumerable<Ingredient?>> GetAll();
        Task<IEnumerable<Ingredient?>> GetByName(string name);
        Task<Ingredient?> GetById(Guid id);
        Task Add(Ingredient? ingredient);
        Task Update(Ingredient? ingredient);
        Task Delete(Guid id);
    }
}