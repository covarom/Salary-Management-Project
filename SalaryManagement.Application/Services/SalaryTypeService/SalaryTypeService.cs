using SalaryManagement.Api.Common.Helper;
using SalaryManagement.Application.Common.Interfaces.Persistence;
using SalaryManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryManagement.Application.Services.SalaryTypeService
{
    public class SalaryTypeService : ISalaryTypeService
    {
        private readonly ISalaryTypeRepository _repository;
        public SalaryTypeService(ISalaryTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<SalaryType> AddSalaryType(SalaryType salaryType)
        {
            return await _repository.AddSalaryType(salaryType);
        }

        public async Task<bool> DeleteSalaryType(string id)
        {
            return await _repository.DeleteSalaryType(id);
        }

        public async Task<IEnumerable<SalaryType>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<SalaryType> GetById(string id)
        {
            return await _repository.GetById(id);
        }

        public async  Task<bool> UpdateSalaryType(SalaryType salaryType)
        {
            var existSalaryType = await _repository.GetById(salaryType.SalaryTypeId);
            if(existSalaryType != null)
            {
                existSalaryType.SalaryTypeName = StringHelper.IsNullOrEmpty(salaryType.SalaryTypeName) ? existSalaryType.SalaryTypeName: salaryType.SalaryTypeName;
                existSalaryType.IsDeleted = StringHelper.IsNullOrEmpty(salaryType.IsDeleted.ToString()) ? existSalaryType.IsDeleted : salaryType.IsDeleted;

                return await _repository.UpdateSalaryType(existSalaryType);
            }
            return false;
        }
    }
}
