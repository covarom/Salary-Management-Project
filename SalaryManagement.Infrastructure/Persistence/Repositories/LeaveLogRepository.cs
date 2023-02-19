using Microsoft.EntityFrameworkCore;
using SalaryManagement.Application.Common.Interfaces.Persistence;
using SalaryManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryManagement.Infrastructure.Persistence.Repositories
{
    public class LeaveLogRepository : ILeaveLogRepository
    {
        private readonly SalaryManagementContext _context;

        public LeaveLogRepository(SalaryManagementContext context)
        {
            _context = context;
        }

        public async Task<LeaveLog> CreateNewLeaveLog(LeaveLog leaveLog)
        {
            _context.LeaveLogs.Add(leaveLog);
            var changes = await _context.SaveChangesAsync();
            return changes > 0 ? leaveLog : null;
        }

        public async Task<bool> DeleteLeaveLogById(string leaveTimeId)
        {
            var leaveLog = await _context.LeaveLogs.SingleOrDefaultAsync(x => x.LeaveTimeId.Equals(leaveTimeId));
            if (leaveLog == null) return false;
            leaveLog.IsDeleted = true;
            var changes = await _context.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<IEnumerable<LeaveLog>> GetAllLeaveLogs()
        {
            return await _context.LeaveLogs.ToListAsync();
        }

        public async Task<LeaveLog> GetLeaveLogById(string leaveTimeId)
        {
            return await _context.LeaveLogs.SingleOrDefaultAsync(x => x.LeaveTimeId.Equals(leaveTimeId));
        }

        public async Task<bool> UpdateLeaveLog(LeaveLog leaveLog)
        {
            _context.LeaveLogs.Update(leaveLog);
            int changes = await _context.SaveChangesAsync();
            return changes > 0;
        }
    }
}
