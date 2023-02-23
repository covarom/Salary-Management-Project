
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


        public async Task<bool> UpdateHoliday(Holiday holiday)
        {
            bool check = false;
            _context.Holidays.Update(holiday);

            int change = await _context.SaveChangesAsync();

            if (change > 0)
            {
                check = true;
            }

            return check;
        }
        public async Task<bool> DeleteHoliday(Holiday holiday)
        {
            bool check = false;
            _context.Holidays.Update(holiday);

            int change = await _context.SaveChangesAsync();

            if (change > 0)
            {
                check = true;
            }

            return check;
        }
    }
}
