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

        //List<Holiday> AddHoliday(Holiday holiday);

        List<Holiday> RemoveHoliday(String id);

        List<Holiday> UpdateHoliday(String id, Holiday request);

    }
}
