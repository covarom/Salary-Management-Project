namespace SalaryManagement.Application.Services.OverTimeServices
{
    public interface IOvertimeService
    {
        Task<int> GetOvertimeHoursAsync(string employeeId, DateTime date);
    }
}
