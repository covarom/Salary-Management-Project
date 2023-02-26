namespace SalaryManagement.Application.Services.HolidayServices
{

    public record HolidayRequest
    {
        public DateTime? StartDate { get; init; }
        public DateTime? EndDate { get; init; }
        public string? HolidayName { get; init; }
        public bool IsDelete { get; init; }
    }


    public record HolidayUpdate
    {
        public DateTime? StartDate { get; init; }
        public DateTime? EndDate { get; init; }
        public string? HolidayName { get; init; }
        public bool IsPaid { get; init; }
    }


    public record HolidayDelete(
        string Id
        );

}
