using Microsoft.EntityFrameworkCore;
using SalaryManagement.Application.Common.Interfaces.Persistence;
using SalaryManagement.Domain.Entities;

namespace SalaryManagement.Infrastructure.Persistence.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {

        private readonly SalaryManagementContext _context;

        public EmployeeRepository(SalaryManagementContext context)
        {
            _context = context;
        }

        public async Task<Employee> AddEmployee(Employee Employee)
        {
            _context.Employees.Add(Employee);
            await _context.SaveChangesAsync();
            return Employee;
        }

        public async Task<Employee> GetById(string id)
        {   
            return await _context.Employees.SingleOrDefaultAsync(x => x.EmployeeId.Equals(id) && x.IsActive == true);
        }

        // public async Employee GetEmployeeByName(string name)
        // {
        //     var Employee =await _context.Employees.SingleOrDefault(x => x.EmployeeIdName.Equals(name));
        //     return Employee;
        // }
        public async Task<IEnumerable<Employee>> GetAllEmployee()
        {
            return await _context.Employees.Where(x => x.IsActive == true).ToListAsync();
        }  

        public async Task<bool> RemoveEmployee(string id)
        {
            bool check=false;
            var Employee = await _context.Employees.FindAsync(id);
            Employee.IsActive = false;
             _context.Employees.Update(Employee);      
            int changes = await _context.SaveChangesAsync();
            if(changes>0){
                check= true;
            }
            return check;
        }

         public async Task<bool> UpdateEmployee(Employee Employee)
        {
            bool check=false;
            _context.Employees.Update(Employee);
             int changes = await _context.SaveChangesAsync();
            if(changes>0){
                check= true;
            }
             return check;

        }

        public async Task<string> GetEmployeeIdByName(string EmployeeName)
        {
            Employee employee = null;
            employee = _context.Employees.SingleOrDefault(e => e.Name.Contains(EmployeeName.Trim()));
            return employee.EmployeeId;
        }
         public async Task<int> CountEmployee()
        {
            var num = await _context.Employees.CountAsync();
            return num;
        }
    }
}
