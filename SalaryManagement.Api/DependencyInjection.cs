using Microsoft.AspNetCore.Mvc.Infrastructure;
using SalaryManagement.Api.Common.Errors;
using SalaryManagement.Api.Common.Mapping;

namespace SalaryManagement.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddSingleton<ProblemDetailsFactory, SalaryManagementProblemDetailsFactory>();

            services.AddMapping();
            return services;
        }
    }
}
