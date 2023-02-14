using Microsoft.AspNetCore.Mvc;
using SalaryManagement.Application.Services.LeaveLogServices;

namespace SalaryManagement.Api.Controllers
{
    [Route("api/v1/leaveLog")]
    [ApiController]
    public class LeaveLogController : ControllerBase
    {
        private readonly ILeaveLogService _leaveLogService;
        
        public LeaveLogController(ILeaveLogService leaveLogService)
        {
            _leaveLogService = leaveLogService;
        }

        [HttpGet("getAllLeaveLogs")]
        public async Task<IActionResult> GetAllLeaaveLogs()
        {
            var leaveLogs = await _leaveLogService.GetAllLeaveLogs();
            if(leaveLogs== null)
            {
                return NotFound();
            }
            return Ok(leaveLogs);
        }

        public async Task<IActionResult> GetLeaveLogsById(string id)
        {
            var leaveLog = await _leaveLogService.GetLeaveLogById(id);
            if(leaveLog== null)
            {
                return NotFound();
            }
            return Ok(leaveLog);
        }
    }
}
