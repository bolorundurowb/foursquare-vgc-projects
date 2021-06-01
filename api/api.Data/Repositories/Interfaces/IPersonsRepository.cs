﻿using System.Threading.Tasks;
using api.Data.Models;

namespace api.Data.Repositories.Interfaces
{
    public interface IPersonsRepository : IRepository<Person>
    {
        Task<bool> CheckByPhone(string phoneNumber);
    }
}