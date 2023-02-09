using System;
using System.Collections.Generic;

namespace SalaryManagement.Domain.Entities;

public partial class OvertimeLog
{
    public string OvertimeId { get; set; } = null!;

    public DateTime? OvertimeDay { get; set; }

    public string? Hours { get; set; }

    public string? Status { get; set; }

    public string? IsDelete { get; set; }

    public string? EmployeeId { get; set; }

    public virtual Employee? Employee { get; set; }
}
