using Mapster;
using SalaryManagement.Contracts.LeaveLog;
using SalaryManagement.Domain.Entities;

namespace SalaryManagement.Api.Common.Mapping
{
    public class LeaveLogMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<LeaveLog, LeaveLogResponseV2>()
                 .Map(dest => dest.LeaveLogId, src => src.LeaveTimeId)
                 .Map(dest => dest.Date, src => src.LeaveDate)
                 .Map(dest => dest.Hours, src => src.LeaveHours)
                 .Map(dest => dest.Reason, src => src.Reason);
                 //.Map(dest => dest.Employee, src => src.Employee);
        }
    }
}
