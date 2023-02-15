
using SalaryManagement.Application.Common.Interfaces.Persistence;
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

        public async Task<IEnumerable<Contract>> GetAllContracts()
        {
            return await _contractRepository.GetAllContractsAsync();
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
