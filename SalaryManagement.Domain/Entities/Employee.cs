using System;
using System.Collections.Generic;

namespace SalaryManagement.Domain.Entities;

public partial class Employee
{
    public string EmployeeId { get; set; } = null!;

    public string? Name { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string? Address { get; set; }

    public string? IdentifyNumber { get; set; }

    public string? IsActive { get; set; }

    public string? PhoneNumber { get; set; }

    public string? CompanyId { get; set; }

    public virtual Company? Company { get; set; }

    public virtual ICollection<Contract> Contracts { get; } = new List<Contract>();

    public virtual ICollection<LeaveLog> LeaveLogs { get; } = new List<LeaveLog>();

    public virtual ICollection<OvertimeLog> OvertimeLogs { get; } = new List<OvertimeLog>();

    public virtual ICollection<Payroll> Payrolls { get; } = new List<Payroll>();
}
