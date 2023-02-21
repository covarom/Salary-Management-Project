using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalaryManagement.Application.Services.EmployeeServices;
using SalaryManagement.Application.Services.LeaveLogServices;
using SalaryManagement.Application.Services.OverTimeServices;
using SalaryManagement.Application.Services.SalaryServices;
using SalaryManagement.Contracts.Salary;

namespace SalaryManagement.Api.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class CalculatingSalaryController : ControllerBase
    {

        private readonly IEmployeeServices _employeeService;
        private readonly ILeaveLogService _leaveLogService;
        private readonly IOvertimeService _overtimeService;
        private readonly ISalaryService _salaryService;

        public CalculatingSalaryController(IEmployeeServices employeeServices, ISalaryService salaryService,
            IOvertimeService overtimeService, ILeaveLogService leaveLogService)
        {
            _employeeService = employeeServices;
            _salaryService = salaryService;    
            _leaveLogService = leaveLogService;
            _overtimeService= overtimeService;
        }

        [HttpGet("{id}/staff-salary")]
        public async Task<ActionResult<SalaryResponse>> GetEmployeeSalary(string id)
        {
            try
            {
                var employee = await _employeeService.GetById(id);

                if (employee == null)
                {
                    return NotFound();
                }

                var otTime = await _overtimeService.GetOvertimeHoursAsync(id);

                var leaveDays = await _leaveLogService.GetTotalLeaveDateByEmployeeIdInMonthAcsyn(id);

                var salaryResponse = await _salaryService.CalculateSalaryAsync(employee, otTime, leaveDays);

                return Ok(salaryResponse);
            }
            catch (Exception ex)
            {
                // log the exception
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
