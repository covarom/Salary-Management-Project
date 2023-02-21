namespace SalaryManagement.Application.Services.HolidayServices
{
    public record HolidayRequest(
        DateTime StartDate,
        DateTime EndDate,
        string HolidayName,
        bool IsDelete
        );

    public record HolidayUpdate(
        string Id,
        DateTime StartDate,
        DateTime EndDate,
        string HolidayName
        );

    public record HolidayDelete(
        string Id
        );

}
