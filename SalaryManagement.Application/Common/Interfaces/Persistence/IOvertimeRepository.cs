namespace SalaryManagement.Application.Common.Interfaces.Persistence
{
    public interface IOvertimeRepository
    {
        Task<decimal> GetTotalOvertimeHoursByEmployeeIdAsync(string employeeId);
    }
}
