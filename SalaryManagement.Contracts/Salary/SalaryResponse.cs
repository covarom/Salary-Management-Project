using SalaryManagement.Contracts.Employee;

namespace SalaryManagement.Contracts.Salary
{ 
    public record EmployeeSalaryResponse
    {
        public string EmployeeId { get; init; }

        public string EmployeeName { get; init; }

        public string CompanyName { get; init; }

        public double BasicSalary { get; init; }

        public double? Bhxh { get; init; }

        public double? Bhyt { get; init; }

        public double? Bhtn { get; init; }

        public double? PersonalIncomeTax { get; init; }

        public double TotalSalary { get; init; }
    }

}
