using SalaryManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryManagement.Application.Services.HolidayServices
{
    public interface IHolidayService
    {
        Task<Holiday> GetHolidaysById(string id);

        Task<IEnumerable<Holiday>> GetAllHoliday();

        Task<Holiday> AddHoliday(Holiday holiday);

        Task<bool> DeleteHoliday(string id);

        Task<bool> UpdateHoliday(Holiday holiday);

    }
}
