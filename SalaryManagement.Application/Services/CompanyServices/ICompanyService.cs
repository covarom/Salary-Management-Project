using SalaryManagement.Domain.Entities;

namespace SalaryManagement.Application.Services.CompanyServices
{
    public interface ICompanyServices 
    {
        Task<Company> GetById(string companyId);

        Task<IEnumerable<Company>> GetAllCompanys();

        Task<Company> AddCompany(Company company);

        Task<bool> RemoveCompany(string companyId);

        Task<bool> UpdateCompany(Company company);
    }
}
