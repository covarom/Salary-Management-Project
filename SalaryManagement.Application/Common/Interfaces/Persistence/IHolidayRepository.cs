
using SalaryManagement.Domain.Entities;


namespace SalaryManagement.Application.Common.Interfaces.Persistence
{
    public interface IHolidayRepository
    {
        Task<Holiday> GetHolidayById(string id);
        Task<IEnumerable<Holiday>> GetAllHolliday();
        Task<Holiday> AddHoliday(Holiday holiday);
        Task<bool> UpdateHoliday(Holiday holiday);
        Task<bool> DeleteHoliday(Holiday holiday);

        Task<IEnumerable<Holiday>> GetHolidaysByDateRangeAsync(DateTime startDate, DateTime endDate);

        Task<IEnumerable<Holiday>> SaveHoliday(IEnumerable<Holiday> holidays);

    }
}
