using Mapster;
using SalaryManagement.Contracts.Holidays;
using SalaryManagement.Domain.Entities;

namespace SalaryManagement.Api.Common.Mapping
{
    public class HolidayMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Holiday, HolidayResponse>()
                .Map(dest => dest.HolidayId, src => src.HolidayId)
                .Map(dest => dest.HolidayName, src => src.HolidayName)
                .Map(dest => dest.StartDate, src => src.StartDate)
                .Map(dest => dest.EndDate, src => src.EndDate)
                .Map(dest => dest.IsPaid, src => src.IsPaid);
        }
    }
}
