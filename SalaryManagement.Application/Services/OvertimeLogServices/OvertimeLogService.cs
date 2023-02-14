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

        public Task<OvertimeLog> AddNewOvertimeLog(OvertimeLog overtimeLog)
        {
            return _overtimeLogRepository.AddNewOvertimeLog(overtimeLog);
        }

        public Task DeleteOvertimeLog(string id)
        {
            return _overtimeLogRepository.DeleteOvertimeLog(id);
        }

        public Task<IEnumerable<OvertimeLog>> GetAllOverTimeLogs()
        {
            return _overtimeLogRepository.GetAllOvertimeLogs();
        }

        public Task<OvertimeLog> GetOvertimeLogById(string id)
        {
            return _overtimeLogRepository.GetOvertimeLogById(id);
        }

        public Task<OvertimeLog> UpdateOvertimeLog(OvertimeLog overtimeLog)
        {
            return _overtimeLogRepository.UpdateOvertimeLog(overtimeLog);
        }
    }
}
