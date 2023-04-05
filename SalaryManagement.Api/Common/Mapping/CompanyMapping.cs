using Mapster;
using SalaryManagement.Contracts.Companys;
using SalaryManagement.Domain.Entities;

namespace SalaryManagement.Api.Common.Mapping
{
    public class CompanyMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Company, CompanyResponse>()
                .Map(dest => dest.CompanyId, src => src.CompanyId)
                .Map(dest => dest.CompanyName, src => src.CompanyName)
                .Map(dest => dest.Address, src => src.Address)
                .Map(dest => dest.Email, src => src.Email)
                .Map(dest => dest.Phone, src => src.Phone);
        }
    }
}
