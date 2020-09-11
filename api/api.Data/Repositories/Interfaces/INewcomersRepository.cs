using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.Data.DTOs;
using api.Data.Enums;
using api.Data.Models;

namespace api.Data.Repositories.Interfaces
{
    public interface INewcomersRepository : IRepository<Newcomer>
    {
        Task<List<DateSummaryDto>> GetNewcomersDates();
        
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
