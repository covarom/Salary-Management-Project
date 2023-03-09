using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalaryManagement.Api.Common.Helper;
using SalaryManagement.Application.Services.ContractServices;
using SalaryManagement.Application.Services.EmployeeServices;
using SalaryManagement.Application.Services.LeaveLogServices;
using SalaryManagement.Contracts.LeaveLog;
using SalaryManagement.Domain.Entities;

namespace SalaryManagement.Api.Controllers
{
    [Route("api/v2/leave-log")]
    [ApiController]
    public class LeaveLogV2Controller : ControllerBase
    {
        private readonly ILeaveLogServiceV2 _leaveLogService;
        private readonly IEmployeeServices _employeeService;
        private readonly IContractServices _contractService;
        private readonly IMapper _mapper;

        public LeaveLogV2Controller(ILeaveLogServiceV2 leaveLogService, IMapper mapper,
       IEmployeeServices employeeService, IContractServices contractServices)
        {
            _leaveLogService = leaveLogService;
            _mapper = mapper;
            _employeeService = employeeService;
            _contractService = contractServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLeaveLogs()
        {
            var leaveLogs = await _leaveLogService.GetAll();
            return Ok(leaveLogs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLeaveLogsById(string id)
        {
            if (id.IsNullOrEmpty())
            {
                return BadRequest("Invalid request! Request ID is empty");
            }
            var leaveLog = await _leaveLogService.GetById(id);
            if (leaveLog == null)
            {
                return NotFound();
            }
            return Ok(leaveLog);
        }

        [HttpPost("")]
        public async Task<IActionResult> AddNewLeaveLog(LeaveLogRequest leaveLog)
        {
            await Task.CompletedTask;
            if (!IsValidRequest(leaveLog))
            {
                return BadRequest("Not valid request!");
            }
            //Check empl có hợp đồng và còn còn làm không
            var emp = await _employeeService.GetById(leaveLog.EmployeeId);
            if (emp == null)
            {
                return BadRequest("Employee doesn't exist!");
            }
            var listContract = await _contractService.GetContractsByEmployeeIdAsync(emp.EmployeeId);
            if (listContract == null)
            {
                return BadRequest("Employee doesn't have contract!");
            }
           /* var log = new LeaveLog
            {
                LeaveTimeId = Guid.NewGuid().ToString(),
               *//* StartDate = leaveLog.startDate,
                EndDate = leaveLog.endDate,*//*
                Reason = leaveLog.Reason,
                IsDeleted = false,
                EmployeeId = leaveLog.EmployeeId,
            };*/

            var result = _leaveLogService.Add(leaveLog);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLeaveLog(string id,LeaveLogRequest leaveLog)
        {
            if (!IsValidRequest(leaveLog))
            {
                return BadRequest("Wrong condition");
            }
            if (leaveLog.EmployeeId.IsNullOrEmpty())
            {
                return BadRequest("EmployeeId do not empty!");
            }

            try
            {

                await _leaveLogService.Update(id, leaveLog);

                return Ok("Update Successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLeaveLog(string id)
        {
            if (id.IsNullOrEmpty())
            {
                return BadRequest();
            }

            try
            {
                await _leaveLogService.Delete(id);
                return Ok();
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            
        }

        private static bool IsValidRequest(UpdateLeaveLogRequest request)
        {
            if (request.Equals(null))
            {
                return false;
            }
            if (request.startDate > DateTime.Now || request.startDate > request.endDate)
            {
                return false;
            }
            return true;
        }

        private static bool IsValidRequest(LeaveLogRequest request)
        {
            if (request == null)
            {
                return false;
            }
            if (request.LeaveHours > 8 || request.LeaveDate > DateTime.UtcNow)
            {
                return false;
            }
            return true;
        }
    }
}
