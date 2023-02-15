namespace SalaryManagement.Contracts.Companys
{
    public record CompanyRequest(
        string company_name
        );
    public record CompanyUpdate(
        string id,
        string company_name
        );    
    public record CompanyDelete(
        string id
        );  
}
