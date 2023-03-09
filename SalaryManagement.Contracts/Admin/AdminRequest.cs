namespace SalaryManagement.Contracts.Admin
{
    public record AdminRequest (
       // string adminId,
        string? Name,
        string? Image,
        string? PhoneNumber,
        string? Email
        );
}
