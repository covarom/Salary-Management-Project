namespace SalaryManagement.Application.Services.PayrollService
{
    public record PayrollRequest
    (
        double Total,
        double Tax,
        string Note,
        DateTime Date,
        bool IsDelate,
        string EmployeeName
    );
    public record PayrollUpdate
    (
        string Id,
        double Total,
        double Tax,
        string Note,
        DateTime Date,
        bool IsDelate,
        string EmployeeId
    );

    public record PayrollDelete
    (
        string Id
    );

}
