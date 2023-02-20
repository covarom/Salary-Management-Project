
using SalaryManagement.Api.Common.Helper;
using SalaryManagement.Application.Common.Interfaces.Persistence;
using SalaryManagement.Contracts;
using SalaryManagement.Contracts.Contracts;
using SalaryManagement.Domain.Contracts;
using SalaryManagement.Domain.Entities;
using Mapster; 

namespace SalaryManagement.Application.Services.ContractServices
{
    public class ContractService : IContractServices
    {
        private readonly IContractRepository _contractRepository;

        public ContractService(IContractRepository contractRepository)
        {
            _contractRepository = contractRepository;
        }

        public async Task<ContractResponse> AddContractAsync(ContractRequest request)
        {
            var contract = request.Adapt<Contract>();
            contract.ContractId = Guid.NewGuid().ToString();
            contract.CreatedAt = DateTime.Now;

            await _contractRepository.AddAsync(contract);
            await _contractRepository.SaveChangesAsync();

            var response = contract.Adapt<ContractResponse>();
            return response;
        }

        public async Task UpdateContract(Contract contractToUpdate)
        {
            contractToUpdate.UpdatedAt = DateTime.Now;

            await _contractRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteContractAsync(string id)
        {
                var contract = await _contractRepository.GetContractById(id);

                if (contract == null)
                {
                    return false;
                }

               return await _contractRepository.DeleteContractAsync(contract);   
        }

       /* public async Task<PaginatedResponse<Contract>> GetAllContracts(int page, int pageSize, string? sortColumn, bool? isDescending = false, string? keyword = null)
        {
           // Get the contracts from the repository
            var response = await _contractRepository.GetContractsAsync(page, pageSize, keyword, sortColumn, isDescending);
            return response;
        }*/

        public async Task<ContractResponse?> GetById(string contractId)
        {
            return await _contractRepository.GetContractByIdAsync(contractId);
        }

      /*  public async Task<Contract?> UpdateContractAsync(string id, Contract contract)
        {
            var existingContract = await _contractRepository.GetContractByIdAsync(id);

            if (existingContract == null)
            {
                return null;
            }

            *//* if(!contract.File.IsNullOrEmpty())
             {
                 existingContract.File = contract.File.Trim();
             }*//*

            existingContract.File = (contract.File.IsNullOrEmpty()) ? existingContract.File : contract.File.Trim();
            existingContract.StartDate = (contract.StartDate == null) ? existingContract.StartDate : contract.StartDate;
            existingContract.EndDate = (contract.StartDate == null) ? existingContract.StartDate : contract.StartDate;
            existingContract.Job = (contract.Job.IsNullOrEmpty()) ? existingContract.Job : contract.Job.Trim();
            existingContract.BasicSalary = (contract.BasicSalary == null) ? existingContract.BasicSalary : contract.BasicSalary;
            existingContract.Bhxh = contract.Bhxh == null ? existingContract.Bhxh : contract.Bhxh;
            existingContract.PartnerId = contract.PartnerId.IsNullOrEmpty() ? existingContract.PartnerId : contract.PartnerId.Trim();
            existingContract.PartnerPrice = contract.PartnerPrice == null ? existingContract.PartnerPrice : contract.PartnerPrice;
            existingContract.EmployeeId = contract.EmployeeId.IsNullOrEmpty() ? existingContract.EmployeeId : contract.EmployeeId.Trim();
      *//*      existingContract.ContractStatus = contract.ContractStatus == null ? existingContract.ContractTypeId : contract.ContractTypeId.Trim();
            existingContract.SalaryTypeId = contract.SalaryTypeId.IsNullOrEmpty() ? existingContract.SalaryTypeId : contract.SalaryTypeId.Trim();
            existingContract.ContractStatusId = contract.ContractStatusId.IsNullOrEmpty() ? existingContract.ContractStatusId : contract.ContractStatusId.Trim();*//*

            var updatedContract = await _contractRepository.UpdateContractAsync(existingContract);
            return updatedContract;
        }*/


        public async Task<PaginatedResponse<ContractResponse>> GetAllContracts(int pageNumber, int pageSize, string? sortBy, bool isDesc, string? searchKeyword)
        {
            return await _contractRepository.GetAllContracts(pageNumber, pageSize, sortBy, isDesc, searchKeyword);
        }

        public async Task<Contract?> GetContractById(string contractId)
        {
            return await _contractRepository.GetContractById(contractId);
        }
    }
}
