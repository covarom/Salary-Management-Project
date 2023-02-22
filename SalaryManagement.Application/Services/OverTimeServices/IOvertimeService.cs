namespace SalaryManagement.Application.Services.OverTimeServices
{
    public interface IOvertimeService
    {
        Task<decimal> GetOvertimeHoursAsync(string employeeId);
    }
}
