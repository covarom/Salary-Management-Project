namespace SalaryManagement.Contracts.OvertimeLog
{
    public record OvertimeLogRequest(
        DateTime OvertimeDate,
        int Hours,
        string Status,
        string EmployeeId
    );

    public record OTUpdateRequest(
        string Id,
        DateTime? OvertimeDate,
        int? Hours,
        string? Status,
        string? EmployeeId
    );
}

