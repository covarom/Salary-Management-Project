using Microsoft.EntityFrameworkCore;
using SalaryManagement.Application.Common.Interfaces.Persistence;
using SalaryManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryManagement.Infrastructure.Persistence.Repositories
{
    public class SalaryTypeRepository : ISalaryTypeRepository
    {
        private readonly SalaryManagementContext _context;

        public SalaryTypeRepository(SalaryManagementContext context)
        {
            _context = context;
        }
        public async Task<SalaryType> AddSalaryType(SalaryType salaryType)
        {
            _context.SalaryTypes.Add(salaryType);
            await _context.SaveChangesAsync();
            return salaryType;
        }

        public async Task<bool> DeleteSalaryType(string id)
        {
            bool check = false;
            var salaryType = await _context.SalaryTypes.FindAsync(id);
            _context.SalaryTypes.Remove(salaryType);

            int change = await _context.SaveChangesAsync();

            if (change > 0)
            {
                check = true;
            }

            return check;
        }

        public async Task<IEnumerable<SalaryType>> GetAll()
        {
            return await _context.SalaryTypes.ToListAsync();
        }

        public async Task<SalaryType> GetById(string id)
        {
            return await _context.SalaryTypes.SingleOrDefaultAsync(x => x.SalaryTypeId.Equals(id));
        }

        public async Task<bool> UpdateSalaryType(SalaryType salaryType)
        {   
            bool check = false;
            _context.SalaryTypes.Update(salaryType);
            
            int change = await _context.SaveChangesAsync();

            if (change > 0)
            {
                check = true;
            }

            return check;
        }
    }
}
