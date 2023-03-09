using SalaryManagement.Contracts.LeaveLog;
using SalaryManagement.Domain.Entities;

namespace SalaryManagement.Application.Services.LeaveLogServices
{
    public interface ILeaveLogServiceV2
    {
        Task<IEnumerable<LeaveLogResponseV2>> GetAll();
        Task<LeaveLogResponseV2> GetById(string id);
        Task<LeaveLogResponseV2> Add(LeaveLogRequest leaveLog);
        Task<LeaveLogResponseV2> Update(string id, LeaveLogRequest leaveLog);
        Task Delete(string id);

    }
}
