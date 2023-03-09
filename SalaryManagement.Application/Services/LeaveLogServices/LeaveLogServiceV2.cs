using Mapster;
using MapsterMapper;
using SalaryManagement.Application.Common.Exception;
using SalaryManagement.Application.Common.Interfaces.Persistence;
using SalaryManagement.Contracts.LeaveLog;
using SalaryManagement.Domain.Entities;
using System.Globalization;

namespace SalaryManagement.Application.Services.LeaveLogServices
{
    public class LeaveLogServiceV2 : ILeaveLogServiceV2
    {
        private readonly ILeaveLogRepositoryV2 _leaveLogRepositoryV2;
        private readonly IMapper _mapper;

        public LeaveLogServiceV2(ILeaveLogRepositoryV2 leaveLogRepositoryV2, IMapper mapper)
        {
            _leaveLogRepositoryV2 = leaveLogRepositoryV2;
            _mapper = mapper;
        }

        public async Task<LeaveLogResponseV2> Add(LeaveLogRequest leaveLog)
        {
            // Map request to entity
            var entity = _mapper.Map<LeaveLog>(leaveLog);

            // Set additional fields as needed
            entity.LeaveTimeId = Guid.NewGuid().ToString();
            entity.Status = "Aprroved";

            // Save entity using repository
            await _leaveLogRepositoryV2.Add(entity);

            // Return saved entity
            return entity.Adapt<LeaveLogResponseV2>();

        }

        public async Task Delete(string id)
        {
            var leaveLog = await _leaveLogRepositoryV2.GetById(id);

            if (leaveLog == null)
            {
                throw new NotFoundException($"Leave log with id {id} not found.");
            }

            leaveLog.IsDeleted = true;

            await _leaveLogRepositoryV2.Update(leaveLog);
        }

        public async Task<IEnumerable<LeaveLogResponseV2>> GetAll()
        {
            var list = await _leaveLogRepositoryV2.GetAll();

            return list.Adapt<List<LeaveLogResponseV2>>();
        }

        public async Task<LeaveLogResponseV2> GetById(string id)
        {
            var leaveLog = await _leaveLogRepositoryV2.GetById(id);
            return leaveLog.Adapt<LeaveLogResponseV2>();
        }

        public async Task<LeaveLogResponseV2> Update(string id, LeaveLogRequest leaveLog)
        {
            try
            {
                var entity = await _leaveLogRepositoryV2.GetById(id);

                if (entity == null)
                {
                    throw new NotFoundException($"LeaveLog with ID {id} not found");
                }

                entity.LeaveHours = leaveLog.LeaveHours;
                entity.EmployeeId = leaveLog.EmployeeId;
                entity.LeaveDate = leaveLog.LeaveDate;
                entity.Reason = leaveLog.Reason;

                await _leaveLogRepositoryV2.Update(entity);
                return entity.Adapt<LeaveLogResponseV2>();
            }
            catch (Exception ex)
            {
                throw new Exception("Internal error: " + ex.Message);
            }
        }
    }
}
