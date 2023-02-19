using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryManagement.Contracts.LeaveLog
{
    public record LeaveLogRequest
    (
        string leaveTimeId,
        DateTime startDate,
        DateTime endDate,
        string? reason,
        string status,
        string employeeId,
        bool isDeleted
    );
}
