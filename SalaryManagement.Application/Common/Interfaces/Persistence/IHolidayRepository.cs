using SalaryManagement.Domain.Entities;

namespace SalaryManagement.Application.Common.Interfaces.Persistence
{
    public interface IHolidayRepository
    {
        Task<Holiday> GetHolidayById(string id);
        Task<IEnumerable<Holiday>> GetAllHolliday();
        Task<Holiday> AddHoliday(Holiday holiday);
        Task<Holiday> UpdateHoliday(string id, Holiday request);
        Task<IEnumerable<Holiday>> DeleteHoliday(string id);

    }
}
