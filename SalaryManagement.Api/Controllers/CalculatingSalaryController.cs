
﻿using Microsoft.AspNetCore.Mvc;
using SalaryManagement.Application.Services.ContractServices;

using SalaryManagement.Application.Services.EmployeeServices;
using SalaryManagement.Application.Services.LeaveLogServices;
using SalaryManagement.Application.Services.OverTimeServices;
using SalaryManagement.Application.Services.SalaryServices;
using SalaryManagement.Contracts.Salary;

using System.Net;


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

        private readonly IContractServices _contractServices;

        public CalculatingSalaryController(IEmployeeServices employeeServices, ISalaryService salaryService,
            IOvertimeService overtimeService, ILeaveLogService leaveLogService, IContractServices contractServices)

        {
            _employeeService = employeeServices;
            _salaryService = salaryService;    
            _leaveLogService = leaveLogService;
            _overtimeService= overtimeService;

            _contractServices= contractServices;

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


                var contract = await _contractServices.GetContractByEmployeeId(employee.EmployeeId);

                if (contract == null)
                {
                    var problemDetails = new ProblemDetails
                    {
                        Status = (int)HttpStatusCode.BadRequest,
                        Title = "Bad Request",
                        Detail = "Employee do not applied any contract in the current!"
                    };

                    return new BadRequestObjectResult(problemDetails);
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


        [HttpGet("{id}/partner-salary")]
        public async Task<ActionResult<SalaryResponse>> GetEmployeeSalaryForPartner(string id)
        {
            try
            {
                var employee = await _employeeService.GetById(id);

                if (employee == null)
                {
                    return NotFound();
                }

                var contract = await _contractServices.GetContractByEmployeeId(employee.EmployeeId);

                if (contract == null)
                {
                    var problemDetails = new ProblemDetails
                    {
                        Status = (int)HttpStatusCode.BadRequest,
                        Title = "Bad Request",
                        Detail = "Employee do not applied any contract in the current!"
                    };

                    return new BadRequestObjectResult(problemDetails);
                }

                var otTime = await _overtimeService.GetOvertimeHoursAsync(id);

                var leaveDays = await _leaveLogService.GetTotalLeaveDateByEmployeeIdInMonthAcsyn(id);

                var salaryResponse = await _salaryService.CalculateSalaryForPartnerAsync(employee, otTime, leaveDays);

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