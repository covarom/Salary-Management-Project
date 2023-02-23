using SalaryManagement.Application.Common.Interfaces.Persistence;

namespace SalaryManagement.Application.Services.OverTimeServices
{
    public class OvertimeService : IOvertimeService
    {
        private readonly IOvertimeRepository _repository;

        public OvertimeService(IOvertimeRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> GetOvertimeHoursAsync(string employeeId)
        {
            return await _repository.GetTotalOvertimeHoursByEmployeeIdAsync(employeeId);
        }
    }
}
