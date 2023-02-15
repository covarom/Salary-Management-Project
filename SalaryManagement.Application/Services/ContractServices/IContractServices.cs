using SalaryManagement.Domain.Entities;

namespace SalaryManagement.Application.Services.ContractServices
{
    public interface IContractServices 
    {
        Task<Contract> GetById(string contractId);
        Task<IEnumerable<Contract>> GetAllContracts();

        Task UpdateContractAsync(Contract contract);

        Task DeleteContractAsync(string id);

        Task<Contract> AddContractAsync(Contract contract);

    }
}
