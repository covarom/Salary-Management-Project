using SalaryManagement.Contracts.Contracts;
using SalaryManagement.Contracts.LeaveLog;
using SalaryManagement.Contracts.OverTimeLog;
using SalaryManagement.Contracts.Payroll;

namespace SalaryManagement.Contracts.Employees
{
    public record EmployeeResponse
    {
        public string EmployeeId { get; init; }
        public string? Name { get; init; }
        public string? Image { get; init; }
        public DateTime? DateOfBirth { get; init; }
        public string? Address { get; init; }
        public int? IdentifyNumber { get; init; }
        public bool? IsActive { get; init; }
        public string? PhoneNumber { get; init; }
       /* public ICollection<ContractResponse> Contracts { get; init; } = new List<ContractResponse>();
        public ICollection<LeaveLogResponse> LeaveLogs { get; init; } = new List<LeaveLogResponse>();
        public ICollection<OvertimeLogResponse> OvertimeLogs { get; init; } = new List<OvertimeLogResponse>();
        public ICollection<PayrollResponse> Payrolls { get; init; } = new List<PayrollResponse>();*/
    }
}
