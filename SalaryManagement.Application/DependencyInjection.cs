
using Microsoft.Extensions.DependencyInjection;
using SalaryManagement.Application.Services.Authentication;

namespace SalaryManagement.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationServices, AuthenticationServices>();
            return services;
        }
    }
}
