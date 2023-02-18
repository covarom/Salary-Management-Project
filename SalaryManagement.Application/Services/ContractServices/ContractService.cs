
using SalaryManagement.Api.Common.Helper;
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

        public async Task<Contract?> GetById(string contractId)
        {
            return await _contractRepository.GetContractByIdAsync(contractId);
        }

        public async Task<Contract?> UpdateContractAsync(string id, Contract contract)
        {
            var existingContract = await _contractRepository.GetContractByIdAsync(id);

            if (existingContract == null)
            {
                return null;
            }

            /* if(!contract.File.IsNullOrEmpty())
             {
                 existingContract.File = contract.File.Trim();
             }*/

            existingContract.File = (contract.File.IsNullOrEmpty()) ? existingContract.File : contract.File.Trim();
            existingContract.StartDate = (contract.StartDate == null) ? existingContract.StartDate : contract.StartDate;
            existingContract.EndDate = (contract.StartDate == null) ? existingContract.StartDate : contract.StartDate;
            existingContract.Job = (contract.Job.IsNullOrEmpty()) ? existingContract.Job : contract.Job.Trim();
            existingContract.BasicSalary = (contract.BasicSalary == null) ? existingContract.BasicSalary : contract.BasicSalary;
            existingContract.Bhxh = contract.Bhxh == null ? existingContract.Bhxh : contract.Bhxh;
            existingContract.PartnerId = contract.PartnerId.IsNullOrEmpty() ? existingContract.PartnerId : contract.PartnerId.Trim();
            existingContract.PartnerPrice = contract.PartnerPrice == null ? existingContract.PartnerPrice : contract.PartnerPrice;
            existingContract.EmployeeId = contract.EmployeeId.IsNullOrEmpty() ? existingContract.EmployeeId : contract.EmployeeId.Trim();
            existingContract.ContractTypeId = contract.ContractTypeId.IsNullOrEmpty() ? existingContract.ContractTypeId : contract.ContractTypeId.Trim();
            existingContract.SalaryTypeId = contract.SalaryTypeId.IsNullOrEmpty() ? existingContract.SalaryTypeId : contract.SalaryTypeId.Trim();
            existingContract.ContractStatusId = contract.ContractStatusId.IsNullOrEmpty() ? existingContract.ContractStatusId : contract.ContractStatusId.Trim();

            var updatedContract = await _contractRepository.UpdateContractAsync(existingContract);
            return updatedContract;
        }
    }
}
