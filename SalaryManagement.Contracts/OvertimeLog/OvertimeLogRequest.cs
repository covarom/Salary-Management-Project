namespace SalaryManagement.Contracts.OvertimeLog
{
    public record OvertimeLogRequest(
        string Id,
        DateTime OvertimeDate,
        int Hours,
        string Status,
        string EmployeeId
    );
}

