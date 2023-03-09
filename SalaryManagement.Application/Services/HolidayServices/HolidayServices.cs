
using SalaryManagement.Api.Common.Helper;


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




        public async Task<bool> DeleteHoliday(Holiday holiday)
        {
            var existHoliday = await _repository.GetHolidayById(holiday.HolidayId);
            if (existHoliday != null)
            {
                existHoliday.IsDeleted = false;

                return await _repository.DeleteHoliday(existHoliday);
            }
            return false;
        }




        public async Task<bool> UpdateHoliday(Holiday holiday)
        {
            return await _repository.UpdateHoliday(holiday);
        }

        public async Task<Holiday> AddHoliday(Holiday holiday)
        {
            return await _repository.AddHoliday(holiday);
        }

        public async Task<IEnumerable<Holiday>> SaveHoliday(List<Holiday> holidays)
        {
            return await _repository.SaveHoliday(holidays);
        }
    }
}
