using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryManagement.Contracts.Contracts
{
    public record UpdateContractRequest(
        string ContractId,
        string File,
        DateTime StartDate,
        DateTime EndDate,
        string Job,
        string BasicSalary,
        string BHXH,
        string Partner,
        string PartnerPrice,
        string EmployeeId,
        string ContractTypeId,
        string SalaryTypeId,
        string ContractStatusId
        );
}
