using Microsoft.AspNetCore.Mvc;
using SalaryManagement.Application.Services.OvertimeLogServices;
using SalaryManagement.Contracts.OvertimeLog;
using SalaryManagement.Domain.Entities;

namespace SalaryManagement.Api.Controllers
{
    [Route("api/v1/overtimeLogs")]
    [ApiController]
    public class OvertimeLogController : ControllerBase
    {
        private readonly IOvertimeLogService _overtimeLogService;

        public OvertimeLogController(IOvertimeLogService overtimeLogService)
        {
            _overtimeLogService = overtimeLogService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllOvertimeLogs()
        {
            var overtimeLogs = await _overtimeLogService.GetAllOverTimeLogs();
            return Ok(overtimeLogs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOvertimeLogById(string id)
        {
            var overtimeLog = await _overtimeLogService.GetOvertimeLogById(id);
            if(overtimeLog.Equals(null))
            {
                return NotFound();
            }
            return Ok(overtimeLog);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewOvertimeLog(OvertimeLogRequest overtimeLogRequest)
        {
            await Task.CompletedTask;
            if (!IsValidRequest(overtimeLogRequest))
            {
                return BadRequest();
            }

            var isExist = !_overtimeLogService.GetOvertimeLogById(overtimeLogRequest.EmployeeId).Equals(null);
            if (!isExist)
            {
                return BadRequest();
            }
            var log = new OvertimeLog
            {
                OvertimeId = Guid.NewGuid().ToString(),
                Hours = overtimeLogRequest.Hours,
                Status = overtimeLogRequest.Status,
                OvertimeDay = overtimeLogRequest.OvertimeDate,
                EmployeeId = overtimeLogRequest.EmployeeId,
                IsDeleted = false
            };
            var result = await _overtimeLogService.AddNewOvertimeLog(log);
            if (result.Equals(null))
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOvertimeLog(OvertimeLogRequest overtimeLogRequest)
        {
            if (!IsValidRequest(overtimeLogRequest))
            {
                return BadRequest();
            }

            if (string.IsNullOrEmpty(overtimeLogRequest.Id))
            {
                return BadRequest();
            }

            var tempLog = await _overtimeLogService.GetOvertimeLogById(overtimeLogRequest.Id);
            if (tempLog.Equals(null))
            {
                return BadRequest();
            }

            var log = new OvertimeLog
            {
                OvertimeId = tempLog.OvertimeId,
                Hours = overtimeLogRequest.Hours,
                OvertimeDay = overtimeLogRequest.OvertimeDate,
                Status = overtimeLogRequest.Status,
                EmployeeId = tempLog.EmployeeId
            };
            var result = await _overtimeLogService.UpdateOvertimeLog(log);
            if (result)
            {
                return Ok();
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOvertimeLog(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }

            var result = await _overtimeLogService.DeleteOvertimeLog(id);
            if (result)
            {
                return Ok();
            }

            return NotFound();
        }

        private static bool IsValidRequest(OvertimeLogRequest overtimeLogRequest)
        {
            if (overtimeLogRequest.Equals(null))
            {
                return false;
            }

            if (string.IsNullOrEmpty(overtimeLogRequest.Status)
                || string.IsNullOrEmpty(overtimeLogRequest.EmployeeId))
            {
                return false;
            }

            if (overtimeLogRequest.OvertimeDate.Equals(null) || overtimeLogRequest.OvertimeDate > DateTime.Now)
            {
                return false;
            }

            if (overtimeLogRequest.Hours is < 0 or > 24)
            {
                return false;
            }
            return true;
        }
    }
}
