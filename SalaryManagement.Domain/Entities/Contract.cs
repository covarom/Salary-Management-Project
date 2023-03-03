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

    public string EmployeeId { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public double? Bhyt { get; set; }

    public double? Bhtn { get; set; }

    public string? SalaryType { get; set; }

    public string? ContractStatus { get; set; }

    public string? ContractType { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual ICollection<PaidHistory> PaidHistories { get; } = new List<PaidHistory>();

    public virtual Company Partner { get; set; } = null!;
}
