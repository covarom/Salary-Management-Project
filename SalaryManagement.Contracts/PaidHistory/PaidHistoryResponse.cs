using SalaryManagement.Contracts.Contracts;

namespace SalaryManagement.Contracts.PaidHistory
{
    public class PaidHistoryResponse
    {
        public string PayHistoryId { get; set; }
        public string? Note { get; set; }
        // public string EmployeeId { get; set; }
        public string? PaidType { get; set; }
        public DateTime PaidDate { get; set; }
        public int StandardWorkHours { get; set; }
        public int RealityWorkHours { get; set; }
        public double BaseSalary { get; set; }
        public double BaseSalaryPerHour { get; set; }
        public double Tax { get; set; }
        public double SocialInsurance { get; set; }
        public double AccidentInsurance { get; set; }
        public double HealthInsurance { get; set; }
        public int OvertimeHours { get; set; }
        public double OvetimeSalaryPerHour { get; set; }
        public double TotalBonus { get; set; }
        public double TotalDeductions { get; set; }
        public int LeaveHours { get; set; }
        public double FinalIncome { get; set; }
        public DateTime PeriodStartDate { get; set; }
        public DateTime PeriodEndDate { get; set; }
        public ContractResponse Contract { get; set; }

    }
}
