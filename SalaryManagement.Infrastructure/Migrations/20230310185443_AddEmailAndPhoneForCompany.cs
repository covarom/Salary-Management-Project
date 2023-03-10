using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalaryManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEmailAndPhoneForCompany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "companys",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "phone",
                table: "companys",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "email",
                table: "companys");

            migrationBuilder.DropColumn(
                name: "phone",
                table: "companys");
        }
    }
}
