
using Microsoft.Extensions.DependencyInjection;
using SalaryManagement.Application.Common.Interfaces.Persistence;
using SalaryManagement.Application.Services.Authentication;
using SalaryManagement.Application.Services.ContractServices;
using SalaryManagement.Application.Services.UserServices;
using SalaryManagement.Application.Services.CompanyServices;

namespace SalaryManagement.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationServices, AuthenticationServices>();
            services.AddScoped<IContractServices, ContractService>();
            services.AddScoped<IUserService,UserService>();
            services.AddScoped<ICompanyServices,CompanyService>();

            return services;
        }
    }
}
