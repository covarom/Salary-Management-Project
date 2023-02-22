using SalaryManagement.Contracts;
using SalaryManagement.Domain.Entities;

namespace SalaryManagement.Application.Common.Interfaces.Persistence
{
    public interface IContractRepository
    {
        Task<Contract?> GetContractByIdAsync(string id);
        Task<IEnumerable<Contract>> GetAllContractsAsync();
        Task<Contract> AddContractAsync(Contract contract);
        Task<Contract?> UpdateContractAsync(Contract contract);
        Task DeleteContractAsync(Contract contract);
        Task<PaginatedResponse<Contract>> GetContractsAsync(int pageNumber, int pageSize, string? searchKeyword, string? sortBy, bool? isDesc);


    }
}
