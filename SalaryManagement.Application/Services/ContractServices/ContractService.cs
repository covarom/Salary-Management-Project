
using SalaryManagement.Api.Common.Helper;
using SalaryManagement.Application.Common.Interfaces.Persistence;
using SalaryManagement.Contracts;
using SalaryManagement.Contracts.Contracts;
using SalaryManagement.Domain.Contracts;
using SalaryManagement.Domain.Entities;
using Mapster;
using SalaryManagement.Domain.Common.Enum;

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
            contract.ContractStatus = ContractStatusEnum.Active.ToString();
            contract.CreatedAt = DateTime.Now;

            // Check if the employee already has a contract during the specified period
            var existingContracts = await _contractRepository.GetContractsByEmployeeIdAsync(request.EmployeeId);
            if (existingContracts != null && existingContracts.StartDate <= request.EndDate 
                && existingContracts.EndDate >= request.StartDate)
            {
                throw new Exception("Employee already has a contract during the specified period.");
            }

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

         public async Task<Contract> GetContractByCompanyId(string companyId)

        {
            return await _contractRepository.GetContractByCompanyId(companyId);
        }

        public async Task<PaginatedResponse<ContractResponse>> GetAllContracts(int pageNumber, int pageSize, string? sortBy, bool isDesc, string? searchKeyword)
        {
            return await _contractRepository.GetAllContracts(pageNumber, pageSize, sortBy, isDesc, searchKeyword);
        }


        public async Task<Contract> GetContractById(string contractId)
        {
            return await _contractRepository.GetContractById(contractId);
        }

        public async Task<Contract?> GetContractByEmployeeId(string employeeId)
        {
            return await _contractRepository.GetContractsByEmployeeIdAsync(employeeId);
        }

        public async Task<Contract?> GetContractsByEmployeeIdAsync(string employeeId)
        {
            return await _contractRepository.GetContractsByEmployeeIdAsync(employeeId);
        }

        public async Task<Contract?> GetContractByEmployeeIdAndDate(string employeeId, DateTime date)
        {
            return await _contractRepository.GetContractByEmployeeIdAndDate(employeeId, date);
        }
        public async Task<int> CountContractActive()
        {
            return await _contractRepository.CountContractActive();
        }
        public async Task<int> CountContractExpired()
        {
            return await _contractRepository.CountContractExpired();
        }
    }
}
