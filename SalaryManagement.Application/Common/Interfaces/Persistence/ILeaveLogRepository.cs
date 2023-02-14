using SalaryManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryManagement.Application.Common.Interfaces.Persistence
{
    public interface ILeaveLogRepository
    {
        Task<IEnumerable<LeaveLog>> GetAllLeaveLogs();
        Task<LeaveLog> GetLeaveLogById(string leaveTimeId);
        Task<LeaveLog> CreateNewLeaveLog(LeaveLog leaveLog);
        Task<LeaveLog> UpdateLeaveLog(LeaveLog leaveLog);
        Task DeleteLeaveLogById(string leaveTimeId);
       
    }
}
