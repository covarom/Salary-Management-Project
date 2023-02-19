using SalaryManagement.Domain.Entities;

namespace SalaryManagement.Application.Common.Interfaces.Persistence
{
    public interface IHolidayRepository
    {
        Task<Holiday> GetHolidayById(string id);
        Task<IEnumerable<Holiday>> GetAllHolliday();
        Task<Holiday> AddHoliday(Holiday holiday);
        Task<bool> UpdateHoliday(Holiday holiday);
        Task<bool> DeleteHoliday(string id);
    }
}
