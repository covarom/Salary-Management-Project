using SalaryManagement.Contracts.Employees;
using SalaryManagement.Domain.Common.Enum;
using SalaryManagement.Domain.Entities;
using SalaryManagement.Contracts.Companys;

namespace SalaryManagement.Contracts.Contracts
{
    public record ContractResponse
    {
        public string ContractId { get; init; }

        public string? File { get; init; }

        public DateTime? StartDate { get; init; }

        public DateTime? EndDate { get; init; }

        public string? Job { get; init; }

        public double? BasicSalary { get; init; }

        public double? Bhxh { get; init; }

        public double? Tax { get; init; }

        public string PartnerId { get; init; }

        public double? PartnerPrice { get; init; }

        public string EmployeeId { get; init; }

     /*   public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }*/

        public double? Bhyt { get; init; }

        public double? Bhtn { get; init; }

        public string? SalaryType { get; init; }

        public string? ContractStatus { get; init; }

        public string? ContractType { get; init; }
        public EmployeeResponse Employee { get; init; }
        public CompanyResponse Partner { get; init; }

    }
}
