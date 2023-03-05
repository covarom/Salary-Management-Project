using SalaryManagement.Contracts.Contracts;
using SalaryManagement.Contracts.Employees;

namespace SalaryManagement.Contracts.PaidHistory
{
    public class PaidHistoryResponse
    {
        public string PayHistoryId { get; set; }

        public string EmployeeId { get; set; }

        public string ContractId { get; set; }

        public double BaseSalary { get; set; }

        public int WorkHours { get; set; }

        public int OtHours { get; set; }

        public int LeaveHours { get; set; }

        public double SocialInsurance { get; set; }

        public double AccidentInsurance { get; set; }

        public double HealthInsurance { get; set; }

        public DateTime PaidDate { get; set; }

        public double SalaryAmount { get; set; }

        public double Bonus { get; set; }

        public double Deductions { get; set; }

        public DateTime PayrollPeriodStart { get; set; }

        public DateTime PayrollPeriodEnd { get; set; }

        public string? Note { get; set; }

        public string? PaidType { get; set; }

        // public EmployeeResponse Employee { get; set; }

        public ContractResponse Contract { get; set; }

    }
}
