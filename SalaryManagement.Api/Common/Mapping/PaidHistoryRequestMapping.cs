using Mapster;
using SalaryManagement.Contracts.PaidHistory;
using SalaryManagement.Domain.Entities;

namespace SalaryManagement.Api.Common.Mapping
{
    public class PaidHistoryRequestMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<PaidHistoryRequest, PaidHistory>()
                 .Map(dest => dest.AccidentInsurance, src => src.AccidentInsurance)
                 .Map(dest => dest.BaseSalary, src => src.BaseSalary)
                 .Map(dest => dest.ContractId, src => src.ContractId)
                 .Map(dest => dest.EmployeeId, src => src.EmployeeId)
                 .Map(dest => dest.SalaryAmount, src => src.FinalIncome)
                 .Map(dest => dest.HealthInsurance, src => src.HealthInsurance)
                 .Map(dest => dest.LeaveHours, src => src.LeaveHours)
                 .Map(dest => dest.Note, src => src.Note)
                 .Map(dest => dest.OtHours, src => src.OvertimeHours)
                 .Map(dest => dest.PaidType, src => src.PaidType)
                 .Map(dest => dest.PaidDate, src => src.PaidDate)
                 .Map(dest => dest.PayrollPeriodEnd, src => src.PeriodEndDate)
                 .Map(dest => dest.PayrollPeriodStart, src => src.PeriodStartDate)
                 .Map(dest => dest.WorkHours, src => src.RealityWorkHours)
                 .Map(dest => dest.StandardWorkHours, src => src.StandardWorkHours)
                 .Map(dest => dest.SocialInsurance, src => src.SocialInsurance)
                 .Map(dest => dest.Tax, src => src.Tax)
                 .Map(dest => dest.Bonus, src => src.TotalBonus)
                 .Map(dest => dest.Deductions, src => src.TotalDeductions);
        }
    }
}
