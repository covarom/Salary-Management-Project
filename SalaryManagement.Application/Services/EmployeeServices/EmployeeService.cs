using SalaryManagement.Application.Common.Interfaces.Persistence;
using SalaryManagement.Domain.Entities;

namespace SalaryManagement.Application.Services.EmployeeServices
{
    public class EmployeeService : IEmployeeServices
    {
        private readonly IEmployeeRepository _EmployeeRepository;

        public EmployeeService(IEmployeeRepository EmployeeRepository)
        {
            _EmployeeRepository = EmployeeRepository;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            return await _EmployeeRepository.GetAllEmployee();
        }

        public async Task<Employee> GetById(string EmployeeId)
        {
            return await _EmployeeRepository.GetById(EmployeeId);
        }

         public async Task<Employee> AddEmployee(Employee Employee)
        {
            return await _EmployeeRepository.AddEmployee(Employee);
        }

         public async Task<bool> RemoveEmployee(string EmployeeId)
        {
            return  await _EmployeeRepository.RemoveEmployee(EmployeeId);
        }

         public  async Task<bool> UpdateEmployee(Employee Employee)
        {
            return await _EmployeeRepository.UpdateEmployee(Employee);
        }
    }
}
