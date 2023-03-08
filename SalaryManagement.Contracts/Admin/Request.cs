namespace SalaryManagement.Contracts.Admin
{
    public record AdminUpdateRequest (
        string adminId,
        string? name,
        string? image,
        string? phoneNumber,
        string? email,
        string? password
        );
}
