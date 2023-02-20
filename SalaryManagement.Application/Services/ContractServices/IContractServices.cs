using SalaryManagement.Contracts;
using SalaryManagement.Contracts.Contracts;
using SalaryManagement.Domain.Contracts;
using SalaryManagement.Domain.Entities;

namespace SalaryManagement.Application.Services.ContractServices
{
    public interface IContractServices 
    {
        Task<ContractResponse?> GetById(string contractId);

        Task<Contract?> GetContractById(string contractId);
        // Task<PaginatedResponse<Contract>> GetAllContracts(int page, int pageSize, string sortColumn, bool? isDescending, string keyword = null);

        Task UpdateContract(Contract contractToUpdate);

        Task<bool> DeleteContractAsync(string id);

        Task<ContractResponse> AddContractAsync(ContractRequest request);

        Task<PaginatedResponse<ContractResponse>> GetAllContracts(int pageNumber, int pageSize, string? sortBy, bool isDesc, string? searchKeyword);

        Task<IEnumerable<Contract>>GetContractByCompanyId(string companyId);
    }
}
