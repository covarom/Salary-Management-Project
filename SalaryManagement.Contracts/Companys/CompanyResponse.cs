namespace SalaryManagement.Contracts.Companys
{
    public record CompanyResponse(
        string CompanyId, 
        string? CompanyName, 
        string? Address);

}
