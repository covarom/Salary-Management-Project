using SalaryManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryManagement.Application.Common.Interfaces.Persistence
{
    public interface ISalaryTypeRepository
    {
        Task<SalaryType> GetById(string id);
        Task<IEnumerable<SalaryType>> GetAll();
        Task<SalaryType> AddSalaryType(SalaryType salaryType);
        Task<bool> UpdateSalaryType(SalaryType salaryType);
        Task<bool> DeleteSalaryType(string id);
    }
}
