﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.Data.Enums;
using api.Data.Models;

namespace api.Data.Repositories.Interfaces
{
    public interface INewcomersRepo : IRepository<Newcomer>
    {
        Task<List<DateTime>> GetNewcomerDates();
        
        Task<List<Newcomer>> GetNewcomers(DateTime date);
        
        Task<Newcomer> AddNewcomer(string fullName, string homeAddress, string phone, string email, string birthDay,
            Gender? gender, string ageGroup, string commentsOrPrayers, string howYouFoundUs, MultiChoice? bornAgain,
            MultiChoice? becomeMember, string remarks);
        
        Task<Newcomer> UpdateNewcomer(string id, DateTime? date, string fullName, string homeAddress, string phone, string email, string birthDay,
            Gender? gender, string ageGroup, string commentsOrPrayers, string howYouFoundUs, MultiChoice? bornAgain,
            MultiChoice? becomeMember, string remarks);
        
        Task RemoveNewcomer(string id);
    }
}
