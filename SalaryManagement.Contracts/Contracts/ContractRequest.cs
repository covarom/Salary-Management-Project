using SalaryManagement.Domain.Common.Enum;

namespace SalaryManagement.Domain.Contracts
{
    public record ContractRequest
    {
        public string? File { get; init; }
        public DateTime? StartDate { get; init; }
        public DateTime? EndDate { get; init; }
        public string? Job { get; init; }
        public double? BasicSalary { get; init; }
        public double? Bhxh { get; init; }
        public double? Bhyt { get; init; }
        public double? Bhtn { get; init; }
        public double? Tax { get; init; }
        public string PartnerId { get; init; } = null!;
        public double? PartnerPrice { get; init; }
        public bool? DeletedAt { get; init; }
        public bool? CreatedAt { get; init; }
        public bool? UpdatedAt { get; init; }
        public string EmployeeId { get; init; } = null!;
        public SalaryTypeEnum? SalaryType { get; init; } // Converted to enum
        public ContractStatusEnum? ContractStatus { get; init; } // Converted to enum
        public ContractTypeEnum? ContractType { get; init; } // Converted to enum
    }
}
