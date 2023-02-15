using SalaryManagement.Application.Common.Interfaces.Persistence;
using SalaryManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryManagement.Application.Services.HolidayServices
{
    public class HolidayServices : IHolidayService
    {
        private readonly IHolidayRepository _repository;

        //public async List<Holiday> AddHoliday(Holiday holiday)
        //{
        //    return await _repository.AddHoliday(holiday);
        //}

        public async Task<IEnumerable<Holiday>> GetAllHoliday()
        {
            return await _repository.GetAllHolliday();
        }

        public Task<Holiday> GetHolidaysById(string id)
        {
            throw new NotImplementedException();
        }

        public List<Holiday> RemoveHoliday(string id)
        {
            throw new NotImplementedException();
        }

        public List<Holiday> UpdateHoliday(string id, Holiday request)
        {
            throw new NotImplementedException();
        }
    }
}
