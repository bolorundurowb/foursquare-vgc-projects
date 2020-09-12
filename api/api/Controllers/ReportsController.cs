using System;
using System.Threading.Tasks;
using api.Data.Repositories.Interfaces;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    public class ReportsController : BaseController
    {
        private readonly IAttendanceRepository _attendanceRepo;
        private readonly INewcomersRepository _newcomersRepo;
        
        public ReportsController(IMapper mapper, IAttendanceRepository attendanceRepo, INewcomersRepository newcomersRepo) : base(mapper)
        {
            _attendanceRepo = attendanceRepo;
            _newcomersRepo = newcomersRepo;
        }

        [HttpPost("{date:DateTime")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GenerateReports(DateTime date)
        {
            var attendance = await _attendanceRepo.GetAttendance(date);
            var newcomers = await _newcomersRepo.GetNewcomers(date);

            return Ok();
        }
    }
}
