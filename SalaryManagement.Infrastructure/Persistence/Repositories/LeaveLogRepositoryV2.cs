using Microsoft.EntityFrameworkCore;
using SalaryManagement.Application.Common.Interfaces.Persistence;
using SalaryManagement.Domain.Entities;

namespace SalaryManagement.Infrastructure.Persistence.Repositories
{
    public class LeaveLogRepositoryV2 : ILeaveLogRepositoryV2
    {
        private readonly SalaryManagementContext _context;

        public LeaveLogRepositoryV2(SalaryManagementContext context)
        {
            _context = context;
        }

        public async Task<LeaveLog> Add(LeaveLog leaveLog)
        {
            await _context.LeaveLogs.AddAsync(leaveLog);
            await _context.SaveChangesAsync();
            return leaveLog;
        }

        public async Task<IEnumerable<LeaveLog>> GetAll()
        {
            return await _context.LeaveLogs.Include(x => x.Employee).Where(x => x.IsDeleted != true).ToListAsync();
        }

        public async Task<LeaveLog?> GetById(string id)
        {
            return await _context.LeaveLogs.Include(x => x.Employee).SingleOrDefaultAsync(x => x.LeaveTimeId.Equals(id) && x.IsDeleted != true);
        }

        public async Task Update(LeaveLog leaveLog)
        {
            _context.Entry(leaveLog).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
