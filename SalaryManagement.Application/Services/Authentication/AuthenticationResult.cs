using SalaryManagement.Domain.Entities;

namespace SalaryManagement.Application.Services.Authentication
{
    public record AuthenticationResult(
        Admin Admin,
        string Token
        );
}
