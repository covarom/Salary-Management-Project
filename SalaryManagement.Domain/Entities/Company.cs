using System;
using System.Collections.Generic;

namespace SalaryManagement.Domain.Entities;


public partial class Company
{
    public string CompanyId { get; set; } = null!;

    public string? CompanyName { get; set; }

    public string? Address { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public virtual ICollection<Contract> Contracts { get; } = new List<Contract>();
}
