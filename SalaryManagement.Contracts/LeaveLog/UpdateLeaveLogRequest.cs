using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryManagement.Contracts.LeaveLog
{
    public record UpdateLeaveLogRequest
    (
        string? leaveTimeId,
        DateTime startDate,
        DateTime endDate,
        string? reason,
        string employeeId
    );

    public record CreateLeaveLogRequest(
        string employeeId,
        DateTime startDate,
        DateTime endDate,
        string? reason
    );
}