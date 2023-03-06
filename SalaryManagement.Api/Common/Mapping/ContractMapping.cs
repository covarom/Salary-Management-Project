using Mapster;
using SalaryManagement.Contracts.Contracts;
using SalaryManagement.Domain.Entities;

namespace SalaryManagement.Api.Common.Mapping
{
    public class ContractMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Contract, ContractResponse>()
                .Map(dest => dest.ContractId, src => src.ContractId)
                .Map(dest => dest.File, src => src.File)
                .Map(dest => dest.StartDate, src => src.StartDate)
                .Map(dest => dest.EndDate, src => src.EndDate)
                .Map(dest => dest.Job, src => src.Job)
                .Map(dest => dest.BasicSalary, src => src.BasicSalary)
                .Map(dest => dest.Bhxh, src => src.Bhxh)
                .Map(dest => dest.Bhyt, src => src.Bhyt)
                .Map(dest => dest.Bhtn, src => src.Bhtn)
                .Map(dest => dest.Tax, src => src.Tax)
                .Map(dest => dest.PartnerPrice, src => src.PartnerPrice)
                .Map(dest => dest.SalaryType, src => src.SalaryType)
                .Map(dest => dest.ContractStatus, src => src.ContractStatus)
                .Map(dest => dest.ContractType, src => src.ContractType)
                .Map(dest => dest.Employee, src => src.Employee)
                .Map(dest => dest.Partner, src => src.Partner);
        }
    }
}
