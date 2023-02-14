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

        public Task<LeaveLog> CreateNewLeaveLog(LeaveLog leaveLog)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<LeaveLog>> GetAllLeaveLogs()
        {
            throw new NotImplementedException();
        }

        public Task<LeaveLog> GetLeaveLogById(string leaveTimeId)
        {
            throw new NotImplementedException();
        }
    }
}
