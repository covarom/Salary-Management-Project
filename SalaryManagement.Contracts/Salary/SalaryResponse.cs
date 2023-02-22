using SalaryManagement.Contracts.Companys;
using SalaryManagement.Contracts.Employee;
using SalaryManagement.Contracts.Employees;

namespace SalaryManagement.Contracts.Salary

{
    public record SalaryResponse
    {
        public EmployeeResponse? Employee { get; set; }
        public CompanyResponse? Company { get; set; }
        public decimal StandardWorkHours { get; set; } = 30 * 8;
        public decimal RealityWorkHours { get; set; }
        public double BaseSalary { get; set; }
        public double BaseSalaryPerHour { get; set; }
        public decimal Tax { get; set; }
        public decimal SocialInsurance { get; set; }
        public decimal AccidentInsurance { get; set; }
        public decimal HealthInsurance { get; set; }
        public decimal OvertimeHours { get; set; }
        public double OvetimeSalaryPerHour { get; set; }
        public double TotalBonus { get; set; }
        public double TotalDeductions { get; set; }
     //   public decimal OvertimeSalary { get; set; }
        public decimal LeaveHours { get; set; }
        public decimal FinalIncome { get; set; }
    }
}
   
