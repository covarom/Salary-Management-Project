using Microsoft.AspNetCore.Mvc;
using SalaryManagement.Application.Services.OvertimeLogServices;

using SalaryManagement.Application.Services.EmployeeServices;
using SalaryManagement.Application.Services.ContractServices;

using SalaryManagement.Contracts.OvertimeLog;
using SalaryManagement.Domain.Entities;
using MapsterMapper;
using SalaryManagement.Contracts.OverTimeLog;

namespace SalaryManagement.Api.Controllers
{
    [Route("api/v1/overtimeLogs")]
    [ApiController]
    public class OvertimeLogController : ControllerBase
    {
        private readonly IOvertimeLogService _overtimeLogService;

        private readonly IEmployeeServices _employeeService;
        private readonly IContractServices _contractService;
        private readonly IMapper _mapper;
        public OvertimeLogController(IOvertimeLogService overtimeLogService,
         IEmployeeServices employeeService, IContractServices contractServices, IMapper mapper)
        {
            _overtimeLogService = overtimeLogService;
            _employeeService = employeeService;
            _contractService = contractServices;
            _mapper = mapper;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllOvertimeLogs()
        {
            var overtimeLogs = await _overtimeLogService.GetAllOverTimeLogs();
            var response = _mapper.Map<List<OvertimeLogResponse>>(overtimeLogs);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOvertimeLogById(string id)
        {
            var overtimeLog = await _overtimeLogService.GetOvertimeLogById(id);
            if(overtimeLog.Equals(null))
            {

                return NotFound("Not found the Overtime log");

            }

            var response = _mapper.Map<OvertimeLogResponse>(overtimeLog);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewOvertimeLog(OvertimeLogRequest overtimeLogRequest)
        {
            await Task.CompletedTask;
            if (!IsValidRequest(overtimeLogRequest))
            {
                return BadRequest();

            }         
            //Check có tồn tại OT log chưa

            var isExist = !_overtimeLogService.GetOvertimeLogById(overtimeLogRequest.EmployeeId).Equals(null);
            if (!isExist)
            {
                return BadRequest();
            }

              //Check empl có hợp đồng và còn còn làm không
            var emp = await _employeeService.GetById(overtimeLogRequest.EmployeeId);
            if (emp == null)
            {
                return BadRequest("Employee doesn't exist!");
            }
            var listContract = await _contractService.GetContractsByEmployeeIdAsync(emp.EmployeeId);
            if(listContract == null){
                return BadRequest("Employee doesn't have contract!");
            }

            var log = new OvertimeLog
            {
                OvertimeId = Guid.NewGuid().ToString(),
                Hours = overtimeLogRequest.Hours,
                OvertimeDay = overtimeLogRequest.OvertimeDate,
                EmployeeId = overtimeLogRequest.EmployeeId,
                IsDeleted = false
            };
            var result = await _overtimeLogService.AddNewOvertimeLog(log);
            if (result.Equals(null))
            {
                return NotFound();
            }
            return Ok("Add successfully");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOvertimeLog(OTUpdateRequest overtimeLogRequest)
        {
            if (!IsValidUpdateRequest(overtimeLogRequest))
            {
                return BadRequest();
            }

            if (string.IsNullOrEmpty(overtimeLogRequest.Id))
            {
                return BadRequest("Invalid params");
            }

            var tempLog = await _overtimeLogService.GetOvertimeLogById(overtimeLogRequest.Id);
            if (tempLog.Equals(null))
            {
                return BadRequest("Invalid params");
            }

            var log = new OvertimeLog
            {
                OvertimeId = tempLog.OvertimeId,
                Hours = overtimeLogRequest.Hours,
                OvertimeDay = overtimeLogRequest.OvertimeDate,
                EmployeeId = tempLog.EmployeeId
            };
            var result = await _overtimeLogService.UpdateOvertimeLog(log);
            if (result)
            {
                return Ok("Update successfully");
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOvertimeLog(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Invalid params");
            }

            var result = await _overtimeLogService.DeleteOvertimeLog(id);
            if (result)
            {
                return Ok("Delete successfully");
            }

            return BadRequest("Invalid params");
        }


        private static bool IsValidRequest(OvertimeLogRequest  overtimeLogRequest)
        {
            if (overtimeLogRequest.Equals(null))
            {
                return false;
            }

            if (string.IsNullOrEmpty(overtimeLogRequest.EmployeeId))
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

        private static bool IsValidUpdateRequest(OTUpdateRequest  overtimeLogRequest)
        {
            if (overtimeLogRequest.Equals(null))
            {
                return false;
            }

            if (string.IsNullOrEmpty(overtimeLogRequest.EmployeeId))
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
