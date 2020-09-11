using api.Data.Repositories.Interfaces;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Authorize]
    [Route("v1/attendance")]
    public class AttendanceController : BaseController
    {
        private readonly IAttendanceRepository _attendanceRepo;
        
        public AttendanceController(IMapper mapper, IAttendanceRepository attendanceRepo) : base(mapper)
        {
            _attendanceRepo = attendanceRepo;
        }
    }
}
