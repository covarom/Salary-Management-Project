using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SalaryManagement.Application.Common.Interfaces.Authentication;
using SalaryManagement.Application.Common.Interfaces.Persistence;
using SalaryManagement.Application.Common.Interfaces.Services;
using SalaryManagement.Infrastructure.Authentication;
using SalaryManagement.Infrastructure.Persistence;
using SalaryManagement.Infrastructure.Persistence.Repositories;
using SalaryManagement.Infrastructure.Services;
using System.Text;
namespace SalaryManagement.Insfrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInsfrastructure(this IServiceCollection services,
           ConfigurationManager configuration)
        {
            //  services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
            //  services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddAuth(configuration);
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();          
            services.AddDBContext(configuration);
            //services.AddAutoMapper(typeof(MappingProfile));
            return services;
        }

        public static IServiceCollection AddDBContext(this IServiceCollection services, ConfigurationManager configuration)
        {

           services.AddDbContext<SalaryManagementContext>(options =>
        options.UseMySQL(configuration.GetConnectionString("SalaryManagementDBContext")).EnableSensitiveDataLogging());
           // services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IAdminRepository, AdminRepository>(); 
            services.AddScoped<IContractRepository, ContractRepository>();
            services.AddScoped<ICompanyRepository,CompanyRepository>();

            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IEmployeeRepository,EmployeeRepository>();


            services.AddScoped<IHolidayRepository, HolidayRepository>();
            services.AddScoped<ILeaveLogRepository, LeaveLogRepository>();
            services.AddScoped<IOvertimeLogRepository, OvertimeLogRepository>();
            services.AddScoped<ISalaryTypeRepository, SalaryTypeRepository>();
            services.AddScoped<IPayrollRepository, PayrollRepository>();
            

            return services;
        }

            public static IServiceCollection AddAuth(this IServiceCollection services,
          ConfigurationManager configuration)
        {
            var jwtSettings = new JwtSettings();
            configuration.Bind(JwtSettings.SectionName, jwtSettings);
            services.AddSingleton(Options.Create(jwtSettings));
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

            services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
                });

            return services;
        }

    }
}
