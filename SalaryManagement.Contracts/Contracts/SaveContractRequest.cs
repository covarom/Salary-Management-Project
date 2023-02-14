namespace SalaryManagement.Contracts.Contracts
{
    public record SaveContractRequest(
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
