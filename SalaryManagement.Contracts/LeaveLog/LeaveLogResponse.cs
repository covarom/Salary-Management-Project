using SalaryManagement.Contracts.Employees;

namespace SalaryManagement.Contracts.LeaveLog
{
    public record LeaveLogResponse
    {
        public string LeaveLogId { get; init; }
        public DateTime? FromDate { get; init; }
        public DateTime? ToDate { get; init; }
        public string? Reason { get; init; }
        public bool? Approved { get; init; }
    }

    public record LeaveLogResponseV2
    {
        public string LeaveLogId { get; init; }
        public DateTime Date { get; init; }
        public int Hours { get; init; }
        public string? Reason { get; init; }
        public EmployeeResponse Employee { get; init; }
    }
}
