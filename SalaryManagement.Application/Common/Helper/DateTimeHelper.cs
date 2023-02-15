using System.Globalization;

namespace SalaryManagement.Api.Common.Helper
{
    public static class DateTimeHelper
    {
        public static bool IsWeekend(DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
        }

        public static bool IsWeekday(DateTime date)
        {
            return !IsWeekend(date);
        }

        public static int GetAge(DateTime dateOfBirth)
        {
            var today = DateTime.Today;
            var age = today.Year - dateOfBirth.Year;
            if (dateOfBirth > today.AddYears(-age))
            {
                age--;
            }
            return age;
        }

        public static bool IsSameDate(DateTime date1, DateTime date2)
        {
            return date1.Year == date2.Year && date1.Month == date2.Month && date1.Day == date2.Day;
        }

        public static DateTime? ParseDateTime(string dateString, string format)
        {
            DateTime result;
            if (DateTime.TryParseExact(dateString, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
            {
                return result;
            }
            return null;
        }
    }

}
