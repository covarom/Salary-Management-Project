namespace SalaryManagement.Application.Services.HolidayServices
{
    public record HolidayRequest(
        DateTime StartDate,
        DateTime EndDate,
        bool IsDelete
        );

    public record HolidayUpdate(
        string Id,
        DateTime StartDate,
        DateTime EndDate,
        bool IsDelete
        );

    public record HolidayDelete(
        string Id
        );

}
