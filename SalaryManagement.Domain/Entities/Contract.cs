using System;
using System.Collections.Generic;

namespace SalaryManagement.Domain.Entities;

public partial class Contract
{
    public string ContractId { get; set; } = null!;

    public string? File { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? Job { get; set; }

    public string? BasicSalary { get; set; }

    public string? Bhxh { get; set; }

    public string? Partner { get; set; }

    public string? PartnerPrice { get; set; }

    public string? EmployeeId { get; set; }

    public string? ContractTypeId { get; set; }

    public string? SalaryTypeId { get; set; }

    public string? ContractStatusId { get; set; }

    public virtual ContractStatus? ContractStatus { get; set; }

    public virtual ContractType? ContractType { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual SalaryType? SalaryType { get; set; }
}
