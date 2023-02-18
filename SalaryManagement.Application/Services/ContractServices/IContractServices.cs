using SalaryManagement.Contracts;
using SalaryManagement.Domain.Entities;

namespace SalaryManagement.Application.Services.ContractServices
{
    public interface IContractServices 
    {
        Task<Contract> GetById(string contractId);
        Task<PaginatedResponse<Contract>> GetAllContracts(int page, int pageSize, string sortColumn, bool? isDescending, string keyword = null);

        Task UpdateContractAsync(Contract contract);

        Task DeleteContractAsync(string id);

        Task<Contract> AddContractAsync(Contract contract);

    }
}
