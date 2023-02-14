using SalaryManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryManagement.Application.Services.OvertimeLogServices
{
    public interface IOvertimeLogService
    {
        public Task<OvertimeLog> GetOvertimeLogById(string id);
        public Task<IEnumerable<OvertimeLog>> GetAllOverTimeLogs();
        public Task<OvertimeLog> AddNewOvertimeLog(OvertimeLog overtimeLog);
        public Task<OvertimeLog> UpdateOvertimeLog(OvertimeLog overtimeLog);
        public Task DeleteOvertimeLog(string id);
    }
}
