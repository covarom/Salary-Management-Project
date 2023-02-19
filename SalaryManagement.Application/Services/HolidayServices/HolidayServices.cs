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

        public async Task<bool> DeleteHoliday(string id)
        {
            return await _repository.DeleteHoliday(id);
        }

        public async Task<bool> UpdateHoliday(Holiday holiday)
        {
            //return await _repository.UpdateHoliday(id, request);
            var existHoliday = await _repository.GetHolidayById(holiday.HolidayId);
            if(existHoliday != null)
        {
                existHoliday.StartDate = StringHelper.IsNullOrEmpty(holiday.StartDate.ToString()) ? existHoliday.StartDate : holiday.StartDate;
                existHoliday.EndDate = StringHelper.IsNullOrEmpty(holiday.EndDate.ToString()) ? existHoliday.EndDate : holiday.EndDate;
                existHoliday.IsDeleted = StringHelper.IsNullOrEmpty(holiday.IsDeleted.ToString()) ? existHoliday.IsDeleted : holiday.IsDeleted;

                return await _repository.UpdateHoliday(existHoliday);
            }
            return false;
        }

        public async Task<Holiday> AddHoliday(Holiday holiday)
        {
            return await _repository.AddHoliday(holiday);
        }
    }
}
