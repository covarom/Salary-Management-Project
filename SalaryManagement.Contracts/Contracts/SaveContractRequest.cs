namespace SalaryManagement.Contracts.Contracts
{
    public record SaveContractRequest(
        string File,
        DateTime StartDate,
        DateTime EndDate,
        string Job,
        double BasicSalary,
        double BHXH,
        string PartnerId,
        double PartnerPrice,
        string EmployeeId,
        string ContractType,
        string SalaryType,
        string ContractStatus
        );
}
