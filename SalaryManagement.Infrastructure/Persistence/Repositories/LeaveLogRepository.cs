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
            return await _context.LeaveLogs.Include(x => x.Employee).Where(x => x.IsDeleted != true).ToListAsync();
        }

        public async Task<LeaveLog> GetLeaveLogById(string leaveTimeId)
        {
            return await _context.LeaveLogs.Include(x => x.Employee).SingleOrDefaultAsync(x => x.LeaveTimeId.Equals(leaveTimeId) && x.IsDeleted != true);
        }

        public async Task<bool> UpdateLeaveLog(LeaveLog leaveLog)
        {
            _context.LeaveLogs.Update(leaveLog);
            int changes = await _context.SaveChangesAsync();
            return changes > 0;
        }

        public async Task UpdateAsync(LeaveLog leaveLog)
        {
            _context.Entry(leaveLog).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetTotalLeaveDaysByEmployeeIdAndMonthAsync(string employeeId, DateTime date)
        {
            var startOfMonth = new DateTime(date.Year, date.Month, 1);
            //var today = DateTime.Now.Date;
            var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);

            var listLeaveLog = await _context.LeaveLogs
                .Where(l => l.EmployeeId == employeeId && l.LeaveDate >= startOfMonth && l.LeaveDate <= endOfMonth)
                .ToListAsync();

            //var totalDays = 0;
            var totalHours = 0;
            foreach(var l in listLeaveLog )
            {
                //totalDays += GetNumberOfDays((DateTime)l.StartDate, (DateTime)l.EndDate);
                totalHours += (int)l.LeaveHours;
            }

            //return totalDays;
            return totalHours;
        }

        private int GetNumberOfDays(DateTime startDate, DateTime endDate)
        {
            TimeSpan timeSpan = endDate - startDate;
            return timeSpan.Days + 1;
        }
    }
}
