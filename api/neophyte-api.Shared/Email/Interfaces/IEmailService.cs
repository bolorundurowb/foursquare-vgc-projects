using System.Threading.Tasks;
using neophyte.api.Shared.Email.Models;

namespace neophyte.api.Shared.Email.Interfaces;

public interface IEmailService
{
    Task<bool> SendAsync(string recipient, EmailMessage emailMessage);
}