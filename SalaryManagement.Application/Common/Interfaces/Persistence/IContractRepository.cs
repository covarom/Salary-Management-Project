using SalaryManagement.Contracts;
using SalaryManagement.Contracts.Contracts;
using SalaryManagement.Domain.Entities;

namespace SalaryManagement.Application.Common.Interfaces.Persistence
{
    public interface IContractRepository
    {
        Task<ContractResponse?> GetContractByIdAsync(string id);

        Task<Contract?> GetContractsByEmployeeIdAsync(string employeeId);

        Task<IEnumerable<Contract>> GetAllContractsAsync();
        Task AddAsync(Contract contract);
        Task SaveChangesAsync();
        Task UpdateContract(Contract contractToUpdate);
        Task<bool> DeleteContractAsync(Contract contract);
       // Task<PaginatedResponse<Contract>> GetContractsAsync(int pageNumber, int pageSize, string? searchKeyword, string? sortBy, bool? isDesc);

        Task<PaginatedResponse<ContractResponse>> GetAllContracts(int pageNumber, int pageSize, string? sortBy, bool isDesc, string? searchKeyword);

        Task<Contract?> GetContractById(string contractId);


        Task<Contract>GetContractByCompanyId(string id);

        Task<Contract> GetContractByEmployeeIdAndDate(string id, DateTime date);

    }
}
