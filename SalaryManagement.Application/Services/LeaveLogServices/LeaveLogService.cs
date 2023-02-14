using SalaryManagement.Application.Common.Interfaces.Persistence;
using SalaryManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryManagement.Application.Services.LeaveLogServices
{
    public class LeaveLogService : ILeaveLogService
    {
        private readonly ILeaveLogRepository _leaveLogRepository;

        public LeaveLogService (ILeaveLogRepository leaveLogRepository)
        {
            _leaveLogRepository = leaveLogRepository;
        }

        public Task<LeaveLog> CreateNewLeaveLog(LeaveLog leaveLog)
        {
            return _leaveLogRepository.CreateNewLeaveLog(leaveLog);
        }

        public Task DeleteLeaveLogById(string leaveTimeId)
        {
            return _leaveLogRepository.DeleteLeaveLogById(leaveTimeId);
        }

        public Task<IEnumerable<LeaveLog>> GetAllLeaveLogs()
        {
            return _leaveLogRepository.GetAllLeaveLogs();
        }

        public Task<LeaveLog> GetLeaveLogById(string leaveTimeId)
        {
            return _leaveLogRepository.GetLeaveLogById(leaveTimeId);
        }

        public Task<LeaveLog> UpdateLeaveLog(LeaveLog leaveLog)
        {
            return _leaveLogRepository.UpdateLeaveLog(leaveLog);
        }
    }
}
