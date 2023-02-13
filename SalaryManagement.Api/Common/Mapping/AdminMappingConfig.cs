using Mapster;
using SalaryManagement.Contracts.Admin;
using SalaryManagement.Domain.Entities;

namespace SalaryManagement.Api.Common.Mapping
{
    public class AdminMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Admin, AdminResponse>()
                 .Map(x => x.Name, y => y.Name)
                 .Map(x => x.PhoneNumber, y => y.PhoneNumber)
                 .Map(x => x.Id, y => y.AdminId);
        }
    }
}
