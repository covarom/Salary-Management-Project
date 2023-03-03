namespace SalaryManagement.Contracts.OvertimeLog
{
    public record OvertimeLogRequest(
        DateTime OvertimeDate,
        int Hours,
        string EmployeeId
    );


    public record OTUpdateRequest(
        string Id,
        DateTime? OvertimeDate,
        int? Hours,
        string? EmployeeId
    );

}

