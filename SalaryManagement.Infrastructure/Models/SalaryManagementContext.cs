using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace SalaryManagement.Infrastructure.Models;

public partial class SalaryManagementContext : DbContext
{
/*    private readonly IConfiguration _configuration;
    public SalaryManagementContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }*/

    public SalaryManagementContext(DbContextOptions<SalaryManagementContext> options)
        : base(options)
    {
    }

   /* protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySQL(_configuration.GetConnectionString("SalaryManagementDBContext"));*/

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SalaryManagementContext).Assembly);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
