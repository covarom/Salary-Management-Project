namespace SalaryManagement.Contracts.Holidays
{
    public class HolidayResponse
    {
        public string HolidayId { get; set; } = null!;

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string? HolidayName { get; set; }

        public bool? IsPaid { get; set; }
    }
}
