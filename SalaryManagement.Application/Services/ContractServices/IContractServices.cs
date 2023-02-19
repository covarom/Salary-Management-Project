using SalaryManagement.Contracts;
using SalaryManagement.Contracts.Contracts;
using SalaryManagement.Domain.Entities;

namespace SalaryManagement.Application.Services.ContractServices
{
    public interface IContractServices 
    {
        Task<ContractResponse?> GetById(string contractId);
       // Task<PaginatedResponse<Contract>> GetAllContracts(int page, int pageSize, string sortColumn, bool? isDescending, string keyword = null);

       // Task<Contract?> UpdateContractAsync(string id, Contract contract);

        Task<bool> DeleteContractAsync(string id);

        Task<Contract> AddContractAsync(Contract contract);

        Task<PaginatedResponse<ContractResponse>> GetAllContracts(int pageNumber, int pageSize, string? sortBy, bool isDesc, string? searchKeyword);
    }
}
