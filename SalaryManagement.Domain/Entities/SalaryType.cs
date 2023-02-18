using System;
using System.Collections.Generic;

namespace SalaryManagement.Domain.Entities;

public partial class SalaryType
{
    public string SalaryTypeId { get; set; } = null!;

    public string? SalaryTypeName { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<Contract> Contracts { get; } = new List<Contract>();
}
