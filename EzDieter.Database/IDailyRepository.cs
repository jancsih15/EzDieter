using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using EzDieter.Domain;

namespace EzDieter.Database
{
    public interface IDailyRepository
    {
        Task<IEnumerable<Daily>> GetByUserId(string userId);
        Task<Daily> GetByDate(string userId, DateOnly date);
        Task Add(Daily day);
        Task Update(Daily day);
    }
}