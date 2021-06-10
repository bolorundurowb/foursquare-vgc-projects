using System.Threading.Tasks;
using neophyte.Models.Binding;
using Refit;

namespace neophyte.DataAccess.Interfaces
{
    public interface IAttendanceV2Client
    {
        [Post("/attendance")]
        [Headers("Authorization: Bearer")]
        Task Register([Body] AttendeeV2BindingModel payload);
    }
}
