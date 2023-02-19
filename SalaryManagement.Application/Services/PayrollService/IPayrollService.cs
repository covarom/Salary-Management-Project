using SalaryManagement.Contracts;
using SalaryManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryManagement.Application.Services.PayrollService
{
    public interface IPayrollService
    {
        Task<Payroll> GetById(string id);
        Task<PaginatedResponse<Payroll>> GetAll(int pageNumber, int pageSize, string? sortBy, bool? isDesc, string keyword = null);
        //Task<IEnumerable<Payroll>> GetAll();
        Task<Payroll> AddPayroll(Payroll payroll);
        Task<bool> UpdatePayroll(Payroll payroll);
        Task<bool> DeletePayroll(string id);
    }
}
