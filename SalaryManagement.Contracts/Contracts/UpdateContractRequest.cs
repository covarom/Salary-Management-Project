using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryManagement.Contracts.Contracts
{
    public record UpdateContractRequest(
        string? File,
        DateTime? StartDate,
        DateTime? EndDate,
        string? Job,
        double? BasicSalary,
        double? BHXH,
        string? PartnerId,
        double? PartnerPrice,
        string? EmployeeId,
        string? ContractTypeId,
        string? SalaryTypeId,
        string? ContractStatusId
        );
}
