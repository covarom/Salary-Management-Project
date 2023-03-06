using SalaryManagement.Domain.Entities;

namespace SalaryManagement.Application.Common.Interfaces.Persistence
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetAllCompanys();
        Task<Company> GetById(string companyId);
        Task<Company> AddCompany(Company Company);
        Task<bool> RemoveCompany(string companyId);
        Task<bool> UpdateCompany(Company Company);
        Task<int> CountCompanyPartner();
    }
}