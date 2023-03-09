namespace SalaryManagement.Contracts.LeaveLog
{
    public class LeaveLogRequest
    {
       public string EmployeeId { get; set; } = null!;

       public DateTime? LeaveDate { get; set; }

       public int LeaveHours { get; set; }

       public string? Reason { get; set; }
    }
}
