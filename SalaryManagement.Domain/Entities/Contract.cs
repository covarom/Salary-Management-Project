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

    public double? BasicSalary { get; set; }

    public double? Bhxh { get; set; }

    public double? Tax { get; set; }

    public string PartnerId { get; set; } = null!;

    public double? PartnerPrice { get; set; }

    public bool? IsDeleted { get; set; }

    public string EmployeeId { get; set; } = null!;

    public string ContractTypeId { get; set; } = null!;

    public string SalaryTypeId { get; set; } = null!;

    public string ContractStatusId { get; set; } = null!;

    public virtual ContractStatus ContractStatus { get; set; } = null!;

    public virtual ContractType ContractType { get; set; } = null!;

    public virtual Employee Employee { get; set; } = null!;

    public virtual Company Partner { get; set; } = null!;

    public virtual SalaryType SalaryType { get; set; } = null!;
}
