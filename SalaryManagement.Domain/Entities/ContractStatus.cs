using System;
using System.Collections.Generic;

namespace SalaryManagement.Domain.Entities;

public partial class ContractStatus
{
    public string ContractStatusId { get; set; } = null!;

    public string? StatusName { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<Contract> Contracts { get; } = new List<Contract>();
}
