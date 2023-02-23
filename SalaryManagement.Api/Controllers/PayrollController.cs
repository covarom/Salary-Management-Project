using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalaryManagement.Application.Common.Interfaces.Persistence;
using SalaryManagement.Application.Services.HolidayServices;
using SalaryManagement.Application.Services.PayrollService;
using SalaryManagement.Domain.Entities;
using System.Diagnostics.Contracts;

namespace SalaryManagement.Api.Controllers
{
    [Route("api/v1/")]
    [ApiController]
    [Authorize]
    public class PayrollController : ControllerBase
    {
        private readonly IPayrollService _payrollService;
        private readonly IMapper _mapper;
        private readonly IEmployeeRepository _employeeRepository;

        public PayrollController(IPayrollService payrollService, IMapper mapper, IEmployeeRepository employeeRepository)
        {
            _payrollService = payrollService;
            _mapper = mapper;
            _employeeRepository = employeeRepository;
        }

        [HttpGet("payrolls")]
        public async Task<IActionResult> GetAllPayroll(int pageNumber, int pageSize, string? keyword, string? sortBy, bool isDesc)
        {
            if (pageSize == 0)
            {
                pageSize = 10;
            }

            var payrolls = await _payrollService.GetAll(pageNumber, pageSize, sortBy, isDesc, keyword);

            return Ok(payrolls);
        }

        [HttpGet("payrolls/{payrollId}")]
        public async Task<IActionResult> GetById(string payrollId)
        {
            var payroll = await _payrollService.GetById(payrollId);
            if (payroll == null)
            {
                return NotFound("Holiday not found");
            }
            else
            {
                return Ok(payroll);
            }
        }

        [HttpPost("payrolls")]
        public async Task<IActionResult> AddPayroll(PayrollRequest request)
        {
            await Task.CompletedTask;
            var payroll = new Payroll
            {
                PayrollId = Guid.NewGuid().ToString(),
                Total = request.Total,
                Tax = request.Tax,
                Note = request.Note,
                Date = request.Date,
                IsDeleted = request.IsDelete,
                EmployeeId = request.EmloyeeId,
                TotalDeduction = request.TotalDeduction,
                TotalBonus = request.TotalBonus

            };
            var result = _payrollService.AddPayroll(payroll);
            return Ok(result);

        }

        [HttpPut("payrolls/{payrollId}")]
        public async Task<IActionResult> UpdatePayroll(string payrollId, PayrollUpdate request)
        {
            var existPayroll = await _payrollService.GetById(payrollId);

            if (existPayroll == null)
            {
                return NotFound();
            }

            var payroll = existPayroll;
            if (request.Total.HasValue)
            {
                payroll.Total = request.Total.Value;
            }

            if (request.Tax.HasValue)
            {
                payroll.Tax = request.Tax.Value;
            }

            if (!string.IsNullOrEmpty(request.Note))
            {
                payroll.Note = request.Note;
            }

            if (request.Date.HasValue)
            {
                payroll.Date = request.Date.Value;
            }

            if (!string.IsNullOrEmpty(request.EmployeeId))
            {
                payroll.EmployeeId = request.EmployeeId;
                //payroll.EmployeeId = await _employeeRepository.GetEmployeeIdByName(payroll.Employee.Name);
            }

            if (request.TotalDeduction.HasValue)
            {
                payroll.TotalDeduction = request.TotalDeduction.Value;
            }

            if (request.TotalBonus.HasValue)
            {
                payroll.TotalBonus = request.TotalBonus.Value;
            }
            await _payrollService.UpdatePayroll(payroll);
            return Ok(payroll);

        }

        [HttpDelete("payrolls/{payrollId}")]
        public async Task<IActionResult> DeletePayroll(string payrollId)
        {
            string id = payrollId;
            Payroll payroll = new Payroll
            {
                PayrollId = id,
                IsDeleted = false
            };
            var result = await _payrollService.DeletePayroll(payroll);
            var msg = "";
            if (result)
            {
                msg = "Delete successfully";
            }
            else
            {
                return NotFound();
            };
            return Ok(msg);
        }
    }
}
