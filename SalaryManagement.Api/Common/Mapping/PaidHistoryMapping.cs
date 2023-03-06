using Mapster;
using SalaryManagement.Contracts.PaidHistory;
using SalaryManagement.Domain.Entities;
namespace SalaryManagement.Api.Common.Mapping
{
    public class PaidHistoryMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<PaidHistory, PaidHistoryResponse>()
                 .Map(dest => dest.PayHistoryId, src => src.PayHistoryId)
                 .Map(dest => dest.AccidentInsurance, src => src.AccidentInsurance)
                 .Map(dest => dest.BaseSalary, src => src.BaseSalary)
                 .Map(dest => dest.BaseSalaryPerHour, src => src.BaseSalary / src.StandardWorkHours)
                 .Map(dest => dest.Contract, src => src.Contract)
                 .Map(dest => dest.FinalIncome, src => src.SalaryAmount)
                 .Map(dest => dest.HealthInsurance, src => src.HealthInsurance)
                 .Map(dest => dest.LeaveHours, src => src.LeaveHours)
                 .Map(dest => dest.Note, src => src.Note)
                 .Map(dest => dest.OvertimeHours, src => src.OtHours)
                 .Map(dest => dest.PaidType, src => src.PaidType)
                 .Map(dest => dest.PaidDate, src => src.PaidDate)
                 .Map(dest => dest.PeriodEndDate, src => src.PayrollPeriodEnd)
                 .Map(dest => dest.PeriodStartDate, src => src.PayrollPeriodStart)
                 .Map(dest => dest.RealityWorkHours, src => src.WorkHours)
                 .Map(dest => dest.StandardWorkHours, src => src.StandardWorkHours)
                 .Map(dest => dest.SocialInsurance, src => src.SocialInsurance)
                 .Map(dest => dest.Tax, src => src.Tax)
                 .Map(dest => dest.TotalBonus, src => src.Bonus)
                 .Map(dest => dest.TotalDeductions, src => src.Deductions)
                 .Map(dest => dest.OvetimeSalaryPerHour, src => (src.BaseSalary / src.StandardWorkHours) * 1.5);
        }
    }
}
