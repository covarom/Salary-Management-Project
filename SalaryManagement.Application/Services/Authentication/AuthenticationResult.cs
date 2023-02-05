using SalaryManagement.Domain.Entities;

namespace SalaryManagement.Application.Services.Authentication
{
    public record AuthenticationResult(
        User User,
        string Token
        );
}
