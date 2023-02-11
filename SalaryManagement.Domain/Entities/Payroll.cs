using System;
using System.Collections.Generic;

namespace SalaryManagement.Domain.Entities;

public partial class Payroll
{
    public string PayrollId { get; set; } = null!;

    public string? Total { get; set; }

    public string? Tax { get; set; }

    public string? Note { get; set; }

    public DateTime? Date { get; set; }

    public string? IsDelete { get; set; }

    public string? EmployeeId { get; set; }

    public virtual Employee? Employee { get; set; }
}
