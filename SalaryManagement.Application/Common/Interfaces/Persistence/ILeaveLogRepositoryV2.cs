using SalaryManagement.Domain.Entities;

namespace SalaryManagement.Application.Common.Interfaces.Persistence
{
    public interface ILeaveLogRepositoryV2
    {
        public Task<IEnumerable<LeaveLog>> GetAll();
        public Task<LeaveLog> GetById(string id);
        public Task<LeaveLog> Add(LeaveLog leaveLog);
        public Task Update(LeaveLog leaveLog);
    }
}
