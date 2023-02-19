using Microsoft.EntityFrameworkCore;
using SalaryManagement.Application.Common.Interfaces.Persistence;
using SalaryManagement.Domain.Entities;

namespace SalaryManagement.Infrastructure.Persistence.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {

        private readonly SalaryManagementContext _context;

        public CompanyRepository(SalaryManagementContext context)
        {
            _context = context;
        }

        public async Task<Company> AddCompany(Company Company)
        {
            _context.Companys.Add(Company);
            await _context.SaveChangesAsync();
            return Company;
        }

        public async Task<Company> GetById(string id)
        {   
            return await _context.Companys.SingleOrDefaultAsync(x => x.CompanyId.Equals(id));
        }

        // public async Company GetCompanyByName(string name)
        // {
        //     var company =await _context.Companys.SingleOrDefault(x => x.CompanyIdName.Equals(name));
        //     return company;
        // }
        public async Task<IEnumerable<Company>> GetAllCompanys()
        {
            return await _context.Companys.ToListAsync();
        }  

        public async Task<bool> RemoveCompany(string id)
        {
            bool check=false;
            var company = await _context.Companys.FindAsync(id);
             _context.Companys.Remove(company);      
            int changes = await _context.SaveChangesAsync();
            if(changes>0){
                check= true;
            }
            return check;
        }

         public async Task<bool> UpdateCompany(Company Company)
        {
            bool check=false;

            var company = await _context.Companys.FindAsync(Company.CompanyId);
            company.CompanyName = Company.CompanyName;
            _context.Companys.Update(company);

             int changes = await _context.SaveChangesAsync();
            if(changes>0){
                check= true;
            }
             return check;
        }   

    }
}
