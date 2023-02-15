using Microsoft.EntityFrameworkCore;
using SalaryManagement.Application.Common.Interfaces.Persistence;
using SalaryManagement.Domain.Entities;

namespace SalaryManagement.Infrastructure.Persistence.Repositories
{
    public class HolidayRepository : IHolidayRepository
    {
        private readonly SalaryManagementContext _context;

        public async Task<Holiday> GetHolidayById(string id)
        {
            return await _context.Holidays.FindAsync(id);
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

        public async Task UpdateHoliday(Holiday holiday)
        {
            _context.Holidays.Update(holiday);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteHoliday(string id)
        {
            var holiday = await _context.Holidays.FindAsync(id);
            _context.Holidays.Remove(holiday);
            await _context.SaveChangesAsync();
        }
    }
}
