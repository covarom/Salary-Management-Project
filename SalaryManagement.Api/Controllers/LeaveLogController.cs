using MapsterMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SalaryManagement.Application.Services.LeaveLogServices;
using SalaryManagement.Contracts.LeaveLog;
using SalaryManagement.Domain.Entities;

namespace SalaryManagement.Api.Controllers
{
    [Route("api/v1/leaveLog")]
    [ApiController]
    public class LeaveLogController : ControllerBase
    {
        private readonly ILeaveLogService _leaveLogService;
        private readonly IMapper _mapper;
        
        public LeaveLogController(ILeaveLogService leaveLogService, IMapper mapper)
        {
            _leaveLogService = leaveLogService;
            _mapper = mapper;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllLeaveLogs()
        {
            var leaveLogs = await _leaveLogService.GetAllLeaveLogs();
            return Ok(leaveLogs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLeaveLogsById(string id)
        {
            if(id.IsNullOrEmpty())
            {
                return BadRequest("Invalid request! Request ID is empty");
            }
            var leaveLog = await _leaveLogService.GetLeaveLogById(id);
            if(leaveLog.Equals(null))
            {
                return NotFound();
            }
            return Ok(leaveLog);
        }

        [HttpPost("")]
        public async Task<IActionResult> AddNewLeaveLog(LeaveLogRequest leaveLog)
        {
            await Task.CompletedTask;
            if(!IsValidRequest(leaveLog))
            {
                return BadRequest();
            }
            var log = new LeaveLog
            {
                LeaveTimeId = Guid.NewGuid().ToString(),
                StartDate= leaveLog.startDate,
                EndDate = leaveLog.endDate,
                Reason = leaveLog.reason,
                Status = leaveLog.status,
                IsDeleted = false,
                EmployeeId= leaveLog.employeeId,
            };
            var result = _leaveLogService.CreateNewLeaveLog(log);
            if(!result.Equals(null))
            {
                return Ok(result);
            }
            return NotFound();
        }

        [HttpPut("")]
        public async Task<IActionResult> UpdateLeaveLog(LeaveLogRequest leaveLog)
        {
            if (!IsValidRequest(leaveLog))
            {
                return BadRequest();
            }
            if(leaveLog.employeeId.IsNullOrEmpty())
            {
                return BadRequest();
            }
            var tempLeaveLog = await _leaveLogService.GetLeaveLogById(leaveLog.leaveTimeId);
            if(tempLeaveLog.Equals(null))
            {
                return BadRequest();
            }
            //khong the update object da bi xoa va update trang thai isDelete tu false thanh true (chi co the update khi xoa)
            if (tempLeaveLog.IsDeleted == true || (tempLeaveLog.IsDeleted == false && leaveLog.isDeleted == true))
            {
                return BadRequest();
            }
            var log = new LeaveLog
            {
                LeaveTimeId = leaveLog.leaveTimeId,
                StartDate = leaveLog.startDate,
                EndDate = leaveLog.endDate,
                Reason = leaveLog.reason,
                Status = leaveLog.status,
                IsDeleted = leaveLog.isDeleted,
                EmployeeId = leaveLog.employeeId
            };
            var result = await _leaveLogService.UpdateLeaveLog(log);
            if (result)
            {
                return Ok();
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLeaveLog(string id)
        {
            if (id.IsNullOrEmpty())
            {
                return BadRequest();
            }

            var result = await _leaveLogService.DeleteLeaveLogById(id);
            return Ok();
        }
        
        private static bool IsValidRequest(LeaveLogRequest request)
        {
            if (request.Equals(null))
            {
                return false;
            }
            if (request.startDate < DateTime.Now || request.startDate > request.endDate)
            {
                return false;
            }
            return !request.status.IsNullOrEmpty();
        }
    }
}
