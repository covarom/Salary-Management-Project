
using SalaryManagement.Application.Common.Interfaces.Persistence;
using SalaryManagement.Domain.Entities;

namespace SalaryManagement.Application.Services.CompanyServices
{
    public class CompanyService : ICompanyServices
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<IEnumerable<Company>> GetAllCompanys()
        {
            return await _companyRepository.GetAllCompanys();
        }

        public async Task<Company> GetById(string companyId)
        {
            return await _companyRepository.GetById(companyId);
        }

         public async Task<Company> AddCompany(Company Company)
        {
            return await _companyRepository.AddCompany(Company);
        }

         public async Task<bool> RemoveCompany(string companyId)
        {
            return  await _companyRepository.RemoveCompany(companyId);
        }

         public  async Task<bool> UpdateCompany(Company Company)
        {
            return await _companyRepository.UpdateCompany(Company);
        }
         public  async Task<int> CountCompanyPartner()
        {
            return await _companyRepository.CountCompanyPartner();
        }
    }
}
