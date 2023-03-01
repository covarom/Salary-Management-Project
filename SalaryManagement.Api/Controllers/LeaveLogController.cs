using MapsterMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SalaryManagement.Application.Services.LeaveLogServices;
using SalaryManagement.Application.Services.EmployeeServices;
using SalaryManagement.Application.Services.ContractServices;
using SalaryManagement.Contracts.LeaveLog;
using SalaryManagement.Domain.Entities;

namespace SalaryManagement.Api.Controllers
{
    [Route("api/v1/leaveLog")]
    [ApiController]
    public class LeaveLogController : ControllerBase
    {
        private readonly ILeaveLogService _leaveLogService;
         private readonly IEmployeeServices _employeeService;
        private readonly IContractServices _contractService;
        private readonly IMapper _mapper;
        
        public LeaveLogController(ILeaveLogService leaveLogService, IMapper mapper,
         IEmployeeServices employeeService, IContractServices contractServices)
        {
            _leaveLogService = leaveLogService;
            _mapper = mapper;
            _employeeService =  employeeService;
            _contractService = contractServices;
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
        public async Task<IActionResult> AddNewLeaveLog(CreateLeaveLogRequest leaveLog)
        {
            await Task.CompletedTask;
            if(!IsValidRequest(leaveLog))
            {
                return BadRequest();
            }
              //Check empl có hợp đồng và còn còn làm không
            var emp = await _employeeService.GetById(leaveLog.employeeId);
            if (emp == null)
            {
                return BadRequest("Employee doesn't exist!");
            }
            var listContract = await _contractService.GetContractsByEmployeeIdAsync(emp.EmployeeId);
            if(listContract == null){
                return BadRequest("Employee doesn't have contract!");
            }
            var log = new LeaveLog
            {
                LeaveTimeId = Guid.NewGuid().ToString(),
                StartDate= leaveLog.startDate,
                EndDate = leaveLog.endDate,
                Reason = leaveLog.reason,
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
        public async Task<IActionResult> UpdateLeaveLog(UpdateLeaveLogRequest updateLeaveLog)
        {
            if (!IsValidRequest(updateLeaveLog))
            {
                return BadRequest();
            }
            if(updateLeaveLog.employeeId.IsNullOrEmpty())
            {
                return BadRequest();
            }
            var tempLeaveLog = await _leaveLogService.GetLeaveLogById(updateLeaveLog.leaveTimeId);
            if(tempLeaveLog.Equals(null))
            {
                return BadRequest();
            }
            var log = new LeaveLog
            {
                LeaveTimeId = updateLeaveLog.leaveTimeId,
                StartDate = updateLeaveLog.startDate,
                EndDate = updateLeaveLog.endDate,
                Reason = updateLeaveLog.reason,
                Status = updateLeaveLog.status,
                EmployeeId = updateLeaveLog.employeeId
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
        
        private static bool IsValidRequest(UpdateLeaveLogRequest request)
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
        
        private static bool IsValidRequest(CreateLeaveLogRequest request)
        {
            if (request.Equals(null))
            {
                return false;
            }
            if (request.startDate < DateTime.Now || request.startDate > request.endDate)
            {
                return false;
            }
            return true;
        }
    }
}
