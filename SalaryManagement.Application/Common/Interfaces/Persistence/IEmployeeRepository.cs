using SalaryManagement.Domain.Entities;

namespace SalaryManagement.Application.Common.Interfaces.Persistence
{
    public interface IEmployeeRepository
    {
         Task<IEnumerable<Employee>> GetAllEmployee();
        Task<Employee> GetById(string EmployeeyId);
        Task<Employee> AddEmployee(Employee Employee);
        Task<bool> RemoveEmployee(string EmployeeId);
        Task<bool> UpdateEmployee(Employee Employee);
        Task<string> GetEmployeeIdByName(string EmployeeName);
    }
}
