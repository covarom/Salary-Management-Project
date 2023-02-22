namespace SalaryManagement.Contracts.Contracts
{
    public record ContractResponse(
        string ContractId,
        string File,
        DateTime StartDate,
        DateTime EndDate,
        string Job,
        string BasicSalary,
        string Bhxh,
        string Partner,
        string PartnerPrice,
        string EmployeeId,
        string ContractTypeId,
        string SalaryTypeId,
        string ContractStatusId
        );
}
