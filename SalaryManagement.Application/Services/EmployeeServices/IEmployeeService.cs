using SalaryManagement.Domain.Entities;

namespace SalaryManagement.Application.Services.EmployeeServices
{
    public interface IEmployeeServices 
    {
        Task<Employee> GetById(string EmployeeId);

        Task<IEnumerable<Employee>> GetAllEmployees();

        Task<Employee> AddEmployee(Employee Employee);

        Task<bool> RemoveEmployee(string EmployeeId);

        Task<bool> UpdateEmployee(Employee Employee);
         Task<int> CountEmployee();
         Task<int> CountEmployeeActive();
    }
}
