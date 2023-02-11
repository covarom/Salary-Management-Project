using SalaryManagement.Domain.Entities;

namespace SalaryManagement.Application.Common.Interfaces.Authentication
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
        string GenerateToken(Admin user);
    }
}
