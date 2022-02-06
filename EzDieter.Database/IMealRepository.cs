using System.Collections.Generic;
using System.Threading.Tasks;
using EzDieter.Domain;

namespace EzDieter.Database
{
    public interface IMealRepository
    {
        Task<IEnumerable<Meal>> GetAllPublic();
        Task<IEnumerable<Meal>> GetAllPrivateOfUser(User user);
        Task<Meal> GetById(string id);
        Task<Meal> GetPrivateById(string id);
        Task Add(Meal meal);
        Task AddPrivate(Meal meal);
        Task Update(Meal meal);
        Task UpdatePrivate(Meal meal);
        Task Delete(string id);
        Task DeletePrivate(string id);
    }
}