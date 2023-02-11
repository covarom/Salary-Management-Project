using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalaryManagement.Domain.Entities;

namespace SalaryManagement.Infrastructure.Persistence.Configurations
{
    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            ConfigUsersTable(builder);
        }

        private void ConfigUsersTable(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id).ValueGeneratedNever();

            builder.Property(t => t.FirstName)
                .HasColumnType("nvarchar(200)");

            builder.Property(t => t.LastName)
                .HasColumnType("nvarchar(200)");

            builder.Property(t => t.Email)
                .HasMaxLength(100);

            builder.Property(t => t.Password)
                 .HasMaxLength(100);

        }
    }
}
