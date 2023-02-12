using SalaryManagement.Domain.Entities;

namespace SalaryManagement.Application.Services.CompanyServices
{
    public record CompanyServiceResult(
        Company Company,
        ServiceResponse ServiceResponse
        );
}
