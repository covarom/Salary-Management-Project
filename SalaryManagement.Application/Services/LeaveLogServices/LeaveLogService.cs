using SalaryManagement.Application.Common.Interfaces.Persistence;
using SalaryManagement.Domain.Entities;

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

        public Task<bool> DeleteLeaveLogById(string leaveTimeId)
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

        public Task<bool> UpdateLeaveLog(LeaveLog leaveLog)
        {
            return _leaveLogRepository.UpdateLeaveLog(leaveLog);
        }

        public async Task UpdateAsync(LeaveLog leaveLog)
        {
            await _leaveLogRepository.UpdateAsync(leaveLog);
        }

        public async Task<int> GetTotalLeaveDateByEmployeeIdInMonthAcsyn(string employeeId)
        {
            return await _leaveLogRepository.GetTotalLeaveDaysByEmployeeIdAndMonthAsync(employeeId);
        } 
    }
}
