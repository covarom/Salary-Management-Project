using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SalaryManagement.Application.Services.OvertimeLogServices;

namespace SalaryManagement.Api.Controllers
{
    [Route("api/v1/overtimeLog")]
    [ApiController]
    public class OvertimeLogController : ControllerBase
    {
        private readonly IOvertimeLogService _overtimeLogService;

        public OvertimeLogController(IOvertimeLogService overtimeLogService)
        {
            _overtimeLogService = overtimeLogService;
        }

        [HttpGet("getAllOvertimeLogs")]
        public async Task<IActionResult> GetAllOvertimeLogs()
        {
            var overtimeLogs = await _overtimeLogService.GetAllOverTimeLogs();
            return Ok(overtimeLogs);
        }

        public async Task<IActionResult> GetOvertimeLogById(string id)
        {
            var overtimeLog = await _overtimeLogService.GetOvertimeLogById(id);
            if(overtimeLog == null)
            {
                return NotFound();
            }
            return Ok(overtimeLog);
        }
    }
}
