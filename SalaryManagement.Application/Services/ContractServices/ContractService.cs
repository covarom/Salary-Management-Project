
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

        public async Task<IEnumerable<Contract>> GetAllContracts()
        {
            return await _contractRepository.GetAllContractsAsync();
        }

        public async Task<Contract> GetById(string contractId)
        {
            return await _contractRepository.GetContractByIdAsync(contractId);
        }
    }
}
