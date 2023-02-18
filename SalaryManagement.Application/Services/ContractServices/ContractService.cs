
using SalaryManagement.Application.Common.Interfaces.Persistence;
using SalaryManagement.Contracts;
using SalaryManagement.Domain.Entities;

namespace SalaryManagement.Application.Services.ContractServices
{
    public class ContractService : IContractServices
    {
        private readonly IContractRepository _contractRepository;

        public ContractService(IContractRepository contractRepository)
        {
            _contractRepository = contractRepository;
        }

        public async Task<Contract> AddContractAsync(Contract contract)
        {
            return await _contractRepository.AddContractAsync(contract);
        }

        public async Task DeleteContractAsync(string id)
        {
            var contractToDelete = await GetById(id);
            if (contractToDelete != null)
            {
                await _contractRepository.DeleteContractAsync(contractToDelete);
            }           
        }

        public async Task<PaginatedResponse<Contract>> GetAllContracts(int page, int pageSize, string? sortColumn, bool? isDescending = false, string? keyword = null)
        {
           // Get the contracts from the repository
            var response = await _contractRepository.GetContractsAsync(page, pageSize, keyword, sortColumn, isDescending);
            return response;
        }

        public async Task<Contract> GetById(string contractId)
        {
            return await _contractRepository.GetContractByIdAsync(contractId);
        }

        public async Task UpdateContractAsync(Contract contract)
        {
            await _contractRepository.UpdateContractAsync(contract);
        }
    }
}
