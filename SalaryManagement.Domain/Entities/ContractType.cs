using System;
using System.Collections.Generic;

namespace SalaryManagement.Domain.Entities;

public partial class ContractType
{
    public string ContractTypeId { get; set; } = null!;

    public string? TypeName { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<Contract> Contracts { get; } = new List<Contract>();
}
