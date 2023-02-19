using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SalaryManagement.Domain.Entities;

public partial class Employee
{
    public string EmployeeId { get; set; } = null!;

    public string? Name { get; set; }

    public string? Image { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string? Address { get; set; }

    public int? IdentifyNumber { get; set; }

    public bool? IsActive { get; set; } 

    public string? PhoneNumber { get; set; }

    public virtual ICollection<Contract> Contracts { get; } = new List<Contract>();

    public virtual ICollection<LeaveLog> LeaveLogs { get; } = new List<LeaveLog>();

    public virtual ICollection<OvertimeLog> OvertimeLogs { get; } = new List<OvertimeLog>();

    [JsonIgnore]
    public virtual ICollection<Payroll> Payrolls { get; } = new List<Payroll>();
}
