using SalaryManagement.Api.Common.Helper;
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

            var existEmloyee = await _EmployeeRepository.GetById(Employee.EmployeeId);
            if (existEmloyee != null)
            {
                //Update only the reference fields
                existEmloyee.Name = StringHelper.IsNullOrEmpty(Employee.Name) ? existEmloyee.Name : Employee.Name;
                existEmloyee.Image = StringHelper.IsNullOrEmpty(Employee.Image) ? existEmloyee.Image: Employee.Image;
                existEmloyee.DateOfBirth = Employee.DateOfBirth == null ? existEmloyee.DateOfBirth : Employee.DateOfBirth;
                existEmloyee.Address = StringHelper.IsNullOrEmpty(Employee.Address) ? existEmloyee.Address : Employee.Address;
                existEmloyee.IdentifyNumber = Employee.IdentifyNumber==null ? existEmloyee.IdentifyNumber: Employee.IdentifyNumber;
                existEmloyee.PhoneNumber = StringHelper.IsNullOrEmpty(Employee.PhoneNumber) ? existEmloyee.PhoneNumber : Employee.PhoneNumber;

                return await _EmployeeRepository.UpdateEmployee(existEmloyee);
            }

            return false;
        }

        public async Task<int> CountEmployee()
        {
            return await _EmployeeRepository.CountEmployee();
        }
    }
}
