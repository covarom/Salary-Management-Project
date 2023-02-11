using System;
using System.Collections.Generic;

namespace SalaryManagement.Domain.Entities;

public partial class Company
{
    public string CompanyId { get; set; } = null!;

    public string? CompanyIdName { get; set; }

    public virtual ICollection<Employee> Employees { get; } = new List<Employee>();
}
