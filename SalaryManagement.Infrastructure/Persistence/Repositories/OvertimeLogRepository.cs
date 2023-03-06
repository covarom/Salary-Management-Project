using SalaryManagement.Application.Common.Interfaces.Persistence;
using SalaryManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SalaryManagement.Infrastructure.Persistence.Repositories
{
    public class OvertimeLogRepository : IOvertimeLogRepository
    {
        private readonly SalaryManagementContext _context;

        public OvertimeLogRepository(SalaryManagementContext context)
        {
            _context = context;
        }

        public async Task<OvertimeLog> AddNewOvertimeLog(OvertimeLog overtimeLog)
        {
            _context.OvertimeLogs.Add(overtimeLog);
            var changes = await _context.SaveChangesAsync();
            return changes > 0 ? overtimeLog : null;
        }

        public async Task<bool> DeleteOvertimeLog(string id)
        {
            var log = await _context.OvertimeLogs.SingleOrDefaultAsync(x => x.OvertimeId.Equals(id));
            if (log == null) return false;

            log.IsDeleted = true;
            var changes = await _context.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<IEnumerable<OvertimeLog>> GetAllOvertimeLogs()
        {
            return await _context.OvertimeLogs.Include(x => x.Employee).Where(x => x.IsDeleted != true).ToListAsync();
        }

        public async Task<OvertimeLog> GetOvertimeLogById(string id)
        {
            return await _context.OvertimeLogs.Include(x => x.Employee).SingleOrDefaultAsync(x => x.OvertimeId.Equals(id) && x.IsDeleted != true);
        }

        public async Task<bool> UpdateOvertimeLog(OvertimeLog overtimeLog)
        {
            var existingOvertimeLog = await _context.OvertimeLogs.FindAsync(overtimeLog.OvertimeId);

            if (existingOvertimeLog != null)
            {
                _context.Entry(existingOvertimeLog).CurrentValues.SetValues(overtimeLog);
            }
            else
            {
                _context.OvertimeLogs.Attach(overtimeLog);
                _context.Entry(overtimeLog).State = EntityState.Modified;
            }

            var changes = await _context.SaveChangesAsync();
            return changes > 0;
        }
    }
}