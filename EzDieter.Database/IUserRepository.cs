using System.Collections;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using EzDieter.Domain;

namespace EzDieter.Database
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> GetById(string id);
        Task Add(User user);
        Task Update(User user);
        Task Delete(string id);
    }
}