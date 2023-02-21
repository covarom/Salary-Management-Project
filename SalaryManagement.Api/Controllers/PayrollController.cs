﻿using MapsterMapper;
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
                IsDeleted = request.IsDelete,
                EmployeeId = request.EmloyeeId,
                Employee = await _employeeRepository.GetById(request.EmloyeeId)
            };
            var existPayroll = _employeeRepository.GetById(payroll.PayrollId);
            if(existPayroll != null)
            {
                var result = _payrollService.AddPayroll(payroll);
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
           
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
            Payroll payroll = new Payroll
            {
                PayrollId = request.Id,
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
