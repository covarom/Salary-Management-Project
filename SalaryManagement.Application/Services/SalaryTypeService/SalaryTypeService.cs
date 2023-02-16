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

        public async Task<IEnumerable<SalaryType>> DeleteSalaryType(string id)
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

        public async  Task<SalaryType> UpdateSalaryType(string id, SalaryType request)
        {
            return await _repository.UpdateSalaryType(id, request);
        }
    }
}
