using SalaryManagement.Domain.Entities;

namespace SalaryManagement.Application.Common.Interfaces.Persistence
{
    public interface IContractRepository
    {
        Task<Contract?> GetContractByIdAsync(string id);
        Task<IEnumerable<Contract>> GetAllContractsAsync();
        Task<Contract> AddContractAsync(Contract contract);
        Task UpdateContractAsync(Contract contract);
        Task DeleteContractAsync(Contract contract);

    }
}
