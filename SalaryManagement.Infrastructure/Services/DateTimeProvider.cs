using SalaryManagement.Application.Common.Interfaces.Services;

namespace SalaryManagement.Infrastructure.Services
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
