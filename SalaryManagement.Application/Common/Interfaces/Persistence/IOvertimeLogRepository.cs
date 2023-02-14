using SalaryManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryManagement.Application.Common.Interfaces.Persistence
{
    public interface IOvertimeLogRepository
    {
        public Task<IEnumerable<OvertimeLog>> GetAllOvertimeLogs();
        public Task<OvertimeLog> GetOvertimeLogById(string id);
        public Task<OvertimeLog> AddNewOvertimeLog(OvertimeLog overtimeLog);
        public Task<OvertimeLog> UpdateOvertimeLog(OvertimeLog overtimeLog);
        public Task DeleteOvertimeLog(string id);
    }
}
