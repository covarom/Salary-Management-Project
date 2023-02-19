namespace SalaryManagement.Contracts.Companys
{
    public record CompanyRequest(
        string company_name
        );
    public record CompanyUpdate(
        string id,
        string company_name,
        string company_address
        );    
    public record CompanyDelete(
        string id
        );  
}
