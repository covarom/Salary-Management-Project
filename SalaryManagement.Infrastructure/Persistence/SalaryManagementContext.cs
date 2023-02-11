﻿using Microsoft.EntityFrameworkCore;
using SalaryManagement.Domain.Entities;

namespace SalaryManagement.Infrastructure.Persistence;

public partial class SalaryManagementContext : DbContext
{
    public SalaryManagementContext()
    {
    }

    public SalaryManagementContext(DbContextOptions<SalaryManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Company> Companys { get; set; }

    public virtual DbSet<Contract> Contracts { get; set; }

    public virtual DbSet<ContractStatus> ContractStatuses { get; set; }

    public virtual DbSet<ContractType> ContractTypes { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Holiday> Holidays { get; set; }

    public virtual DbSet<LeaveLog> LeaveLogs { get; set; }

    public virtual DbSet<OvertimeLog> OvertimeLogs { get; set; }

    public virtual DbSet<Payroll> Payrolls { get; set; }

    public virtual DbSet<SalaryType> SalaryTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SalaryManagementContext).Assembly);

        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.AdminId).HasName("PRIMARY");

            entity.ToTable("admins");

            entity.Property(e => e.AdminId).HasColumnName("admin_id");
            entity.Property(e => e.IsActive)
                .HasMaxLength(255)
                .HasColumnName("isActive");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(255)
                .HasColumnName("phone_number");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .HasColumnName("username");
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.CompanyId).HasName("PRIMARY");

            entity.ToTable("companys");

            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.CompanyIdName)
                .HasMaxLength(255)
                .HasColumnName("company_id_name");
        });

        modelBuilder.Entity<Contract>(entity =>
        {
            entity.HasKey(e => e.ContractId).HasName("PRIMARY");

            entity.ToTable("contracts");

            entity.HasIndex(e => e.ContractStatusId, "contract_status_id");

            entity.HasIndex(e => e.ContractTypeId, "contract_type_id");

            entity.HasIndex(e => e.EmployeeId, "employee_id");

            entity.HasIndex(e => e.SalaryTypeId, "salary_type_id");

            entity.Property(e => e.ContractId).HasColumnName("contract_id");
            entity.Property(e => e.BasicSalary)
                .HasMaxLength(255)
                .HasColumnName("basic_salary");
            entity.Property(e => e.Bhxh)
                .HasMaxLength(255)
                .HasColumnName("BHXH");
            entity.Property(e => e.ContractStatusId).HasColumnName("contract_status_id");
            entity.Property(e => e.ContractTypeId).HasColumnName("contract_type_id");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.EndDate)
                .HasColumnType("timestamp")
                .HasColumnName("end_date");
            entity.Property(e => e.File)
                .HasMaxLength(255)
                .HasColumnName("file");
            entity.Property(e => e.Job)
                .HasMaxLength(255)
                .HasColumnName("job");
            entity.Property(e => e.Partner)
                .HasMaxLength(255)
                .HasColumnName("partner");
            entity.Property(e => e.PartnerPrice)
                .HasMaxLength(255)
                .HasColumnName("partner_price");
            entity.Property(e => e.SalaryTypeId).HasColumnName("salary_type_id");
            entity.Property(e => e.StartDate)
                .HasColumnType("timestamp")
                .HasColumnName("start_date");

            entity.HasOne(d => d.ContractStatus).WithMany(p => p.Contracts)
                .HasForeignKey(d => d.ContractStatusId)
                .HasConstraintName("contracts_ibfk_4");

            entity.HasOne(d => d.ContractType).WithMany(p => p.Contracts)
                .HasForeignKey(d => d.ContractTypeId)
                .HasConstraintName("contracts_ibfk_2");

            entity.HasOne(d => d.Employee).WithMany(p => p.Contracts)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("contracts_ibfk_1");

            entity.HasOne(d => d.SalaryType).WithMany(p => p.Contracts)
                .HasForeignKey(d => d.SalaryTypeId)
                .HasConstraintName("contracts_ibfk_3");
        });

        modelBuilder.Entity<ContractStatus>(entity =>
        {
            entity.HasKey(e => e.ContractStatusId).HasName("PRIMARY");

            entity.ToTable("contract_status");

            entity.Property(e => e.ContractStatusId).HasColumnName("contract_status_id");
            entity.Property(e => e.StatusName)
                .HasMaxLength(255)
                .HasColumnName("status_name");
        });

        modelBuilder.Entity<ContractType>(entity =>
        {
            entity.HasKey(e => e.ContractTypeId).HasName("PRIMARY");

            entity.ToTable("contract_types");

            entity.Property(e => e.ContractTypeId).HasColumnName("contract_type_id");
            entity.Property(e => e.TypeName)
                .HasMaxLength(255)
                .HasColumnName("type_name");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PRIMARY");

            entity.ToTable("employees");

            entity.HasIndex(e => e.CompanyId, "company_id");

            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.DateOfBirth)
                .HasColumnType("timestamp")
                .HasColumnName("date_of_birth");
            entity.Property(e => e.IdentifyNumber)
                .HasMaxLength(255)
                .HasColumnName("identify_number");
            entity.Property(e => e.IsActive)
                .HasMaxLength(255)
                .HasColumnName("isActive");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(255)
                .HasColumnName("phone_number");

            entity.HasOne(d => d.Company).WithMany(p => p.Employees)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("employees_ibfk_1");
        });

        modelBuilder.Entity<Holiday>(entity =>
        {
            entity.HasKey(e => e.HolidayId).HasName("PRIMARY");

            entity.ToTable("holidays");

            entity.Property(e => e.HolidayId).HasColumnName("holiday_id");
            entity.Property(e => e.EndDate)
                .HasColumnType("timestamp")
                .HasColumnName("end_date");
            entity.Property(e => e.IsDelete)
                .HasMaxLength(255)
                .HasColumnName("isDelete");
            entity.Property(e => e.StartDate)
                .HasColumnType("timestamp")
                .HasColumnName("start_date");
        });

        modelBuilder.Entity<LeaveLog>(entity =>
        {
            entity.HasKey(e => e.LeaveTimeId).HasName("PRIMARY");

            entity.ToTable("leave_logs");

            entity.HasIndex(e => e.EmployeeId, "employee_id");

            entity.Property(e => e.LeaveTimeId).HasColumnName("leave_time_id");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.EndDate)
                .HasColumnType("timestamp")
                .HasColumnName("end_date");
            entity.Property(e => e.IsDelete)
                .HasMaxLength(255)
                .HasColumnName("isDelete");
            entity.Property(e => e.Reason)
                .HasColumnType("text")
                .HasColumnName("reason");
            entity.Property(e => e.StartDate)
                .HasColumnType("timestamp")
                .HasColumnName("start_date");
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .HasColumnName("status");

            entity.HasOne(d => d.Employee).WithMany(p => p.LeaveLogs)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("leave_logs_ibfk_1");
        });

        modelBuilder.Entity<OvertimeLog>(entity =>
        {
            entity.HasKey(e => e.OvertimeId).HasName("PRIMARY");

            entity.ToTable("overtime_logs");

            entity.HasIndex(e => e.EmployeeId, "employee_id");

            entity.Property(e => e.OvertimeId).HasColumnName("overtime_id");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.Hours)
                .HasMaxLength(255)
                .HasColumnName("hours");
            entity.Property(e => e.IsDelete)
                .HasMaxLength(255)
                .HasColumnName("isDelete");
            entity.Property(e => e.OvertimeDay)
                .HasColumnType("timestamp")
                .HasColumnName("overtime_day");
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .HasColumnName("status");

            entity.HasOne(d => d.Employee).WithMany(p => p.OvertimeLogs)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("overtime_logs_ibfk_1");
        });

        modelBuilder.Entity<Payroll>(entity =>
        {
            entity.HasKey(e => e.PayrollId).HasName("PRIMARY");

            entity.ToTable("payrolls");

            entity.HasIndex(e => e.EmployeeId, "employee_id");

            entity.Property(e => e.PayrollId).HasColumnName("payroll_id");
            entity.Property(e => e.Date)
                .HasColumnType("timestamp")
                .HasColumnName("date");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.IsDelete)
                .HasMaxLength(255)
                .HasColumnName("isDelete");
            entity.Property(e => e.Note)
                .HasColumnType("text")
                .HasColumnName("note");
            entity.Property(e => e.Tax)
                .HasMaxLength(255)
                .HasColumnName("tax");
            entity.Property(e => e.Total)
                .HasMaxLength(255)
                .HasColumnName("total");

            entity.HasOne(d => d.Employee).WithMany(p => p.Payrolls)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("payrolls_ibfk_1");
        });

        modelBuilder.Entity<SalaryType>(entity =>
        {
            entity.HasKey(e => e.SalaryTypeId).HasName("PRIMARY");

            entity.ToTable("salary_types");

            entity.Property(e => e.SalaryTypeId).HasColumnName("salary_type_id");
            entity.Property(e => e.SalaryTypeName)
                .HasMaxLength(255)
                .HasColumnName("salary_type_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}