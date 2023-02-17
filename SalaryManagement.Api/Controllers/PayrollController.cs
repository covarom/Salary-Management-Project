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

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllPayroll(int pageNumber, int pageSize, string? keyword, string? sortBy, bool isDesc)
        {
            if (pageSize == 0)
            {
                pageSize = 10;
            }

            var payrolls = await _payrollService.GetAll(pageNumber, pageSize, sortBy, isDesc, keyword);

            return Ok(payrolls);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var payroll = await _payrollService.GetById(id);
            if (payroll == null)
            {
                return NotFound("Holiday not found");
            }
            else
            {
                return Ok(payroll);
            }
        }

        [HttpPost("")]
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
                IsDeleted = request.IsDelate,
                EmployeeId = await _employeeRepository.GetEmployeeIdByName(request.EmployeeName)
            };
                
            var result = _payrollService.AddPayroll(payroll);
            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Updatepayroll(PayrollUpdate request)
        {
            Payroll payroll = new Payroll
            {
                PayrollId = request.Id,
                Total = request.Total,
                Tax = request.Tax,
                Note = request.Note,
                Date = request.Date,
                IsDeleted = request.IsDelate,
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

        [HttpDelete("delete")]
        public async Task<IActionResult> DeletePayroll(PayrollDelete request)
        {
            string id = request.Id;
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
