using System;
using System.Collections.Generic;

namespace SalaryManagement.Domain.Entities;

public partial class Payroll
{
    public string PayrollId { get; set; } = null!;

    public double? Total { get; set; }

    public double? Tax { get; set; }

    public string? Note { get; set; }

    public DateTime? Date { get; set; }

    public bool? IsDeleted { get; set; }

    public string? EmployeeId { get; set; }

    public double? TotalDeduction { get; set; }

    public double? TotalBonus { get; set; }

    public virtual Employee? Employee { get; set; }
}
