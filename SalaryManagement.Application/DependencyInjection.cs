
using Microsoft.Extensions.DependencyInjection;
using SalaryManagement.Application.Services.Authentication;
using SalaryManagement.Application.Services.UserServices;

namespace SalaryManagement.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationServices, AuthenticationServices>();
            services.AddScoped<IUserService,UserService>();
            return services;
        }
    }
}
