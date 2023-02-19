using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalaryManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddHolidayNameColumnToHoliday : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
           name: "HolidayName",
           table: "holidays",
           nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HolidayName",
                table: "holidays");
        }
    }
}
