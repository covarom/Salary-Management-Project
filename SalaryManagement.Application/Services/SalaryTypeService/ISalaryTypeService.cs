using SalaryManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryManagement.Application.Services.SalaryTypeService
{
    public interface ISalaryTypeService
    {
        Task<SalaryType> GetById(string id);
        Task<IEnumerable<SalaryType>> GetAll();
        Task<SalaryType> AddSalaryType(SalaryType salaryType);
        Task<SalaryType> UpdateSalaryType(string id, SalaryType request);
        Task<IEnumerable<SalaryType>> DeleteSalaryType(string id);
    }
}
