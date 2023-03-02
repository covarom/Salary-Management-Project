namespace SalaryManagement.Contracts.PaidHistory
{
    public class PaidHistoryRequest
    {
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
    }

}
