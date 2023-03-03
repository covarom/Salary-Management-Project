
using SalaryManagement.Domain.Entities;

namespace SalaryManagement.Application.Services.HolidayServices
{
    public record HolidayServicesResult(
        Holiday Holiday,
        ServiceResponse ServiceResponse
        );
}
