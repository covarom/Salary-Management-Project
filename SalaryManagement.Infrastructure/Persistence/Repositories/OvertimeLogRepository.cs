using SalaryManagement.Application.Common.Interfaces.Persistence;
using SalaryManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryManagement.Infrastructure.Persistence.Repositories
{
    public class OvertimeLogRepository : IOvertimeLogRepository
    {
        private readonly SalaryManagementContext _context;

        public OvertimeLogRepository(SalaryManagementContext context)
        {
            _context = context;
        }

        public Task<OvertimeLog> AddNewOvertimeLog(OvertimeLog overtimeLog)
        {
            throw new NotImplementedException();
        }

        public Task DeleteOvertimeLog(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OvertimeLog>> GetAllOvertimeLogs()
        {
            throw new NotImplementedException();
        }

        public Task<OvertimeLog> GetOvertimeLogById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<OvertimeLog> UpdateOvertimeLog(OvertimeLog overtimeLog)
        {
            throw new NotImplementedException();
        }
    }
}
