using SalaryManagement.Domain.Entities;

namespace SalaryManagement.Application.Services.ContractServices
{
    public interface IContractServices 
    {
        Task<Contract> GetById(string contractId);

        Task<IEnumerable<Contract>> GetAllContracts();

       /* bool AddContract(Contract contract);

        bool RemoveContract(string id);

        bool UpdateContract(Contract contract);*/
    }
}
