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
        double TotalDeduction,
        double TotalBonus

    );
    public record PayrollUpdate
    {
        public string? Id { get; init; }
        public double? Total { get; init; }
        public double? Tax { get; init; }
        public string? Note { get; init; }
        public DateTime? Date { get; init; }
        public string? EmployeeId { get; init; }
        public double? TotalDeduction { get; init; }
        public double? TotalBonus { get; init; }
    }

    public record PayrollDelete
    (
        string Id
    );

}
