using System;
using System.Collections.Generic;

namespace SalaryManagement.Domain.Entities;

public partial class LeaveLog
{
    public string LeaveTimeId { get; set; } = null!;

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? Reason { get; set; }

    public string? Status { get; set; }

    public string? IsDelete { get; set; }

    public string? EmployeeId { get; set; }

    public virtual Employee? Employee { get; set; }
}
