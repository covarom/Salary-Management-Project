
using Microsoft.Extensions.DependencyInjection;
using SalaryManagement.Application.Services.AdminServices;
using SalaryManagement.Application.Services.Authentication;
using SalaryManagement.Application.Services.ContractServices;
using SalaryManagement.Application.Services.CompanyServices;

namespace SalaryManagement.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationServices, AuthenticationServices>();
            services.AddScoped<IContractServices, ContractService>();
            services.AddScoped<ICompanyServices,CompanyService>();
            services.AddScoped<IAdminServices, AdminService>();
            

            return services;
        }
    }
}
