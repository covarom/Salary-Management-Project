namespace SalaryManagement.Contracts.OverTimeLog
{
    public record OvertimeLogResponse
    {
        public string OvertimeLogId { get; init; }
        public DateTime? Date { get; init; }
        public double? Duration { get; init; }
        public bool? Approved { get; init; }
    }
}
