using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SalaryManagement.Api;
using SalaryManagement.Application;
using SalaryManagement.Insfrastructure;
using System.Configuration;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions()
{
    //Setting the status of deployment
    EnvironmentName = Microsoft.Extensions.Hosting.EnvironmentName.Development
});

// Load the values from the .env file
DotNetEnv.Env.Load();

// Add services to the container.
builder.Services
    .AddPresentation()
    .AddApplication()
    .AddInsfrastructure(builder.Configuration);

//Config swagger
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddSwaggerGen(c =>
    {
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "insert token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "Bearer"
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                        {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                 },
                 new string[]{}
            }
        });
    });
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler("/error");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
