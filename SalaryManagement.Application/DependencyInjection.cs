
using Microsoft.Extensions.DependencyInjection;
using SalaryManagement.Application.Services.AdminServices;
using SalaryManagement.Application.Services.Authentication;
using SalaryManagement.Application.Services.ContractServices;
using SalaryManagement.Application.Services.CompanyServices;
using SalaryManagement.Application.Services.HolidayServices;
using SalaryManagement.Application.Services.EmployeeServices;
using SalaryManagement.Application.Services.SalaryTypeService;

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
            services.AddScoped<IEmployeeServices, EmployeeService>();

            services.AddScoped<IHolidayService, HolidayServices>();
            services.AddScoped<ISalaryTypeService, SalaryTypeService>();
            return services;
        }
    }
}
