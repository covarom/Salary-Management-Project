using SalaryManagement.Contracts;
using SalaryManagement.Domain.Entities;

namespace SalaryManagement.Application.Common.Interfaces.Persistence
{
    public interface IPayrollRepository
    {
        Task<Payroll> GetById(string id);
        Task<PaginatedResponse<Payroll>> GetAll(int pageNumber, int pageSize, string? keyword, string? sortBy, bool? isDesc);
        Task<Payroll> AddPayroll(Payroll payroll);
        Task<bool> UpdatePayroll(Payroll payroll);
        Task<bool> DeletePayroll(Payroll payroll);
    }
}
