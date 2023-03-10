using SalaryManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryManagement.Application.Services.LeaveLogServices
{
    public interface ILeaveLogService
    {
        Task<IEnumerable<LeaveLog>> GetAllLeaveLogs();
        Task<LeaveLog> GetLeaveLogById(string leaveTimeId);
        Task<LeaveLog> CreateNewLeaveLog(LeaveLog leaveLog);
        Task<bool> UpdateLeaveLog(LeaveLog leaveLog);
        Task<bool> DeleteLeaveLogById(string leaveTimeId);

        Task UpdateAsync(LeaveLog leaveLog);

        Task<int> GetTotalLeaveDateByEmployeeIdInMonthAcsyn(string employeeId, DateTime date);
    }
}
