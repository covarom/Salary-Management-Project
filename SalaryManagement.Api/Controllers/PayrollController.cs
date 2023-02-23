using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalaryManagement.Application.Common.Interfaces.Persistence;
using SalaryManagement.Application.Services.HolidayServices;
using SalaryManagement.Application.Services.PayrollService;
using SalaryManagement.Domain.Entities;

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
                Employee = await _employeeRepository.GetById(request.EmloyeeId)
            };
                
            var result = _payrollService.AddPayroll(payroll);
            return Ok(result);
        }

        [HttpPut("payrolls/{payrollId}")]
        public async Task<IActionResult> Updatepayroll(string payrollId, PayrollUpdate request)
        {
            Payroll payroll = new Payroll
            {
                PayrollId = payrollId,
                Total = request.Total,
                Tax = request.Tax,
                Note = request.Note,
                Date = request.Date,
                EmployeeId = request.EmployeeId
            };
            var result = await _payrollService.UpdatePayroll(payroll);
            var msg = "";
            if (result)
            {
                msg = "Update successfully";
            }
            else
            {
                return NotFound();
            };
            return Ok(msg);
        }

        [HttpDelete("payrolls/{payrollId}")]
        public async Task<IActionResult> DeletePayroll(string payrollId)
        {
            string id = payrollId;
            var msg = "";

            Payroll payroll = await _payrollService.GetById(id);

            if (payroll != null)
            {
                var result = await _payrollService.DeletePayroll(id);
                if (result)
                {
                    msg = "Delete successfully";
                }
                else
                {
                    msg = "Delete failed";
                }
            }
            else
            {
                msg = "Payroll not found";
            }
            return Ok(msg);
        }
    }
}
