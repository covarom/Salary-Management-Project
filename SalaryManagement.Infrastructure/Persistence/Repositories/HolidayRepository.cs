using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using SalaryManagement.Application.Common.Interfaces.Persistence;
using SalaryManagement.Domain.Entities;

namespace SalaryManagement.Infrastructure.Persistence.Repositories
{
    public class HolidayRepository : IHolidayRepository
    {
        private readonly SalaryManagementContext _context;

        public HolidayRepository(SalaryManagementContext context)
        {
            _context = context;
        }

        public async Task<Holiday> GetHolidayById(string id)
        {
            return await _context.Holidays.SingleOrDefaultAsync(x => x.HolidayId.Equals(id));
        }
        public async Task<IEnumerable<Holiday>> GetAllHolliday()
        {
           return await _context.Holidays.ToListAsync();
        }

        public async Task<Holiday> AddHoliday(Holiday holiday)
        {
            _context.Holidays.Add(holiday);
            await _context.SaveChangesAsync();
            return holiday;
        }

        public async Task<Holiday> UpdateHoliday(string id, Holiday request)
        {
            var holiday = await _context.Holidays.FindAsync(id);
            if(holiday == null)
            {
                return null;
            }
            holiday.HolidayId = id;
            holiday.StartDate = request.StartDate;
            holiday.EndDate = request.EndDate;
            holiday.IsDeleted = request.IsDeleted;
            await _context.SaveChangesAsync();

            return holiday;
        }
        public async Task<IEnumerable<Holiday>> DeleteHoliday(string id)
        {
            var holiday = await _context.Holidays.FindAsync(id);
            if (holiday !is null)
            {
                return null;
            }
            _context.Holidays.Remove(holiday);
            await _context.SaveChangesAsync();
            return await _context.Holidays.ToListAsync();
        }
    }
}
