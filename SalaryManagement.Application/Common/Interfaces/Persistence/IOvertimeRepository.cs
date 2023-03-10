namespace SalaryManagement.Application.Common.Interfaces.Persistence
{
    public interface IOvertimeRepository
    {
        Task<int> GetTotalOvertimeHoursByEmployeeIdAsync(string employeeId, DateTime date);
    }
}
