using Mapster;
using SalaryManagement.Application.Services.Authentication;
using SalaryManagement.Contracts.Authentication;

namespace SalaryManagement.Api.Common.Mapping
{
    public class AuthenticationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<AuthenticationResult, AuthenticationResponse>()
                .Map(dest => dest.Token, src => src.Token)
                .Map(dest => dest.Id, src => src.Admin.AdminId)
                .Map(dest => dest.Name, src => src.Admin.Name)
                .Map(dest => dest.UserName, src => src.Admin.Username)
                .Map(dest => dest.PhoneNumber, src => src.Admin.PhoneNumber);
        }
    }
}
