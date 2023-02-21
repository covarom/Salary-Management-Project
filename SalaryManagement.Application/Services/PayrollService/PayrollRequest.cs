using SalaryManagement.Domain.Entities;

namespace SalaryManagement.Application.Services.PayrollService
{
    public record PayrollRequest
    (
        double Total,
        double Tax,
        string Note,
        DateTime Date,
        bool IsDelete,
        string EmloyeeId,
        Employee Employee
    );
    public record PayrollUpdate
    (
        string Id,
        double Total,
        double Tax,
        string Note,
        DateTime Date,
        string EmployeeId
    );

    public record PayrollDelete
    (
        string Id
    );

}
