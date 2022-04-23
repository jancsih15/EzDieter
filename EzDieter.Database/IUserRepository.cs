using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EzDieter.Domain;

namespace EzDieter.Database
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> GetById(Guid? id);
        Task Add(User user);
        Task Update(User user);
        Task Delete(Guid id);
    }
}