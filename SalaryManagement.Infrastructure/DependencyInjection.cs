using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SalaryManagement.Application.Common.Interfaces.Authentication;
using SalaryManagement.Application.Common.Interfaces.Persistence;
using SalaryManagement.Application.Common.Interfaces.Services;
using SalaryManagement.Infrastructure.Authentication;
using SalaryManagement.Infrastructure.Persistence;
using SalaryManagement.Infrastructure.Services;

namespace SalaryManagement.Insfrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInsfrastructure(this IServiceCollection services,
           ConfigurationManager configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}
