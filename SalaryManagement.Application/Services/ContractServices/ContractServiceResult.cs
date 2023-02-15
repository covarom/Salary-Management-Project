using SalaryManagement.Domain.Entities;

namespace SalaryManagement.Application.Services.ContractServices
{
    public record ContractServiceResult(
        Contract Contract,
        ServiceResponse ServiceResponse
        );
}
