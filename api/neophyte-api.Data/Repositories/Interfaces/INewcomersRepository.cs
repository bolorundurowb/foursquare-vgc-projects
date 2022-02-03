using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using neophyte.api.Data.DTOs;
using neophyte.api.Data.Entities;
using neophyte.api.Data.Enums;

namespace neophyte.api.Data.Repositories.Interfaces;

public interface INewcomersRepository 
{
    Task<List<DateSummaryDto>> GetNewcomersDates();

    Task<List<Newcomer>> GetNewcomers(DateTime date);

    Task<Newcomer> AddNewcomer(string fullName, string homeAddress, string phone, string email, string birthDay,
        string ageGroup, string commentsOrPrayers, string howYouFoundUs, MultiChoice? becomeMember,
        Gender? gender = null, MultiChoice? bornAgain = null);

    Task<Newcomer> UpdateNewcomer(string id, DateTime? date, string fullName, string homeAddress, string phone,
        string email, string birthDay,
        Gender? gender, string ageGroup, string commentsOrPrayers, string howYouFoundUs, MultiChoice? bornAgain,
        MultiChoice? becomeMember, string remarks);

    Task RemoveNewcomer(string id);
}