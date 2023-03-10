namespace SalaryManagement.Contracts.Companys
{
    public record CompanyRequest(
        string company_name,
        string? address,
        string? email,
        string? phone
        );
    public record CompanyUpdate(
        string id,
        string? company_name,
        string? company_address,
        string? email,
        string? phone
        );    
    public record CompanyDelete(
        string id
        );  
}
