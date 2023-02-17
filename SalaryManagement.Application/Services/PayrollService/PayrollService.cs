﻿using SalaryManagement.Api.Common.Helper;
using SalaryManagement.Application.Common.Interfaces.Persistence;
using SalaryManagement.Contracts;
using SalaryManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryManagement.Application.Services.PayrollService
{
    public class PayrollService : IPayrollService
    {
        private readonly IPayrollRepository _repository;
        private readonly IEmployeeRepository _employeeRepository;

        public PayrollService(IPayrollRepository repository, IEmployeeRepository employeeRepository)
        {
            _repository = repository;
            _employeeRepository = employeeRepository;
        }

        public async Task<Payroll> AddPayroll(Payroll payroll)
        {
            return await _repository.AddPayroll(payroll);
        }

        public async Task<bool> DeletePayroll(string id)
        {
            return await _repository.DeletePayroll(id);
        }

        public async Task<Payroll> GetById(string id)
        {
            return await _repository.GetById(id);
        }

        public async Task<PaginatedResponse<Payroll>> GetAll(int pageNumber, int pageSize, string? sortBy, bool? isDesc, string keyword = null)
        {
            var response = await _repository.GetAll(pageNumber, pageSize, keyword, sortBy, isDesc);
            return response;
        }

        public async Task<bool> UpdatePayroll(Payroll payroll)
        {
            var existPayroll = await _repository.GetById(payroll.PayrollId);
            if (existPayroll != null) 
            {
                existPayroll.Total = StringHelper.IsNullOrEmpty(payroll.Total.ToString()) ? existPayroll.Total : payroll.Total;
                existPayroll.Tax = StringHelper.IsNullOrEmpty(payroll.Tax.ToString()) ? existPayroll.Tax : payroll.Tax;
                existPayroll.Note = StringHelper.IsNullOrEmpty(payroll.Note) ? existPayroll.Note : payroll.Note;
                existPayroll.Date = payroll.Date == null ? existPayroll.Date : payroll.Date;
                existPayroll.Employee.Name = StringHelper.IsNullOrEmpty(payroll.Employee.Name) ? existPayroll.EmployeeId : payroll.EmployeeId = await _employeeRepository.GetEmployeeIdByName(payroll.Employee.Name);

                return await _repository.UpdatePayroll(existPayroll);
            }

            return false;
        }
    }
}