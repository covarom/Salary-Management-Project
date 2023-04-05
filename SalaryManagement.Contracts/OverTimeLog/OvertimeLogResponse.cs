using SalaryManagement.Contracts.Employees;

namespace SalaryManagement.Contracts.OverTimeLog
{
    public record OvertimeLogResponse
    {
        public string OvertimeId { get; set; } = null!;

        public DateTime? OvertimeDay { get; set; }

        public int? Hours { get; set; }

        public string? Status { get; set; }

        public string? EmployeeId { get; set; }

        public virtual EmployeeResponse? Employee { get; set; }
    }
}
