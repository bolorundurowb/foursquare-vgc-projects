using System.Threading.Tasks;
using api.Shared.Email.Models;

namespace api.Shared.Email.Interfaces;

public interface IEmailService
{
    Task<bool> SendAsync(string recipient, EmailMessage emailMessage);
}