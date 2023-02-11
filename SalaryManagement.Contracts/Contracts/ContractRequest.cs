namespace SalaryManagement.Contracts.Contracts
{
    public record ContractRequest(
        string Id,
        string File,
        DateTime StartDate,
        DateTime EndDate,
        string Job,
        string BacsicSalary,
        string BHXH,
        string Partner,
        string PartnerPrice,
        string EmployeeId,
        string ContractTypeId,
        string SalaryTypeId,
        string ContractStatusId
        );
}
