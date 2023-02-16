using SalaryManagement.Application.Common.Interfaces.Persistence;
using SalaryManagement.Domain.Entities;

namespace SalaryManagement.Application.Services.HolidayServices
{
    public class HolidayServices : IHolidayService
    {
        private readonly IHolidayRepository _repository;

        public HolidayServices(IHolidayRepository holidayRepository)
        {
            _repository = holidayRepository;
        }

        public async Task<IEnumerable<Holiday>> GetAllHoliday()
        {
            return await _repository.GetAllHolliday();
        }

        public async Task<Holiday> GetHolidaysById(string id)
        {
            return await _repository.GetHolidayById(id);
        }

        public async Task<IEnumerable<Holiday>> DeleteHoliday(string id)
        {
            return await _repository.DeleteHoliday(id);
        }

        public async Task<Holiday> UpdateHoliday(string id, Holiday request)
        {
            return await _repository.UpdateHoliday(id, request);
        }

        public async Task<Holiday> AddHoliday(Holiday holiday)
        {
            return await _repository.AddHoliday(holiday);
        }
    }
}
