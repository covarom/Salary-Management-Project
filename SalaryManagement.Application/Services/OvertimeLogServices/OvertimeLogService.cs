using SalaryManagement.Application.Common.Interfaces.Persistence;
using SalaryManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryManagement.Application.Services.OvertimeLogServices
{
    public class OvertimeLogService : IOvertimeLogService
    {
        private readonly IOvertimeLogRepository _overtimeLogRepository;
        public OvertimeLogService(IOvertimeLogRepository overtimeLogRepository)
        {
            _overtimeLogRepository = overtimeLogRepository;
        }

        public async Task<OvertimeLog> AddNewOvertimeLog(OvertimeLog overtimeLog)
        {
            return await _overtimeLogRepository.AddNewOvertimeLog(overtimeLog);
        }

        public async Task<bool> DeleteOvertimeLog(string id)
        {
            return await _overtimeLogRepository.DeleteOvertimeLog(id);
        }

        public async Task<IEnumerable<OvertimeLog>> GetAllOverTimeLogs()
        {
            return await _overtimeLogRepository.GetAllOvertimeLogs();
        }

        public async Task<OvertimeLog> GetOvertimeLogById(string id)
        {
            return await _overtimeLogRepository.GetOvertimeLogById(id);
        }

        public async Task<bool> UpdateOvertimeLog(OvertimeLog overtimeLog)
        {
            return await _overtimeLogRepository.UpdateOvertimeLog(overtimeLog);
        }
    }
}
