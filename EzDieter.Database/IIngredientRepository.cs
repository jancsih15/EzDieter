using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using EzDieter.Domain;

namespace EzDieter.Database
{
    public interface IIngredientRepository
    {
        Task<IEnumerable<Ingredient>> GetAll();
        Task<Ingredient> GetById(string id);
        Task Add(Ingredient ingredient);
        Task Update(Ingredient ingredient);
        Task Delete(string id);
    }
}