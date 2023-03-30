using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalaryManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FirstInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "admins",
                columns: table => new
                {
                    adminid = table.Column<string>(name: "admin_id", type: "varchar(255)", nullable: false),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    image = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    phonenumber = table.Column<string>(name: "phone_number", type: "varchar(20)", maxLength: 20, nullable: true),
                    email = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    isActive = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    username = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    password = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    isfirstlogin = table.Column<bool>(name: "is_first_login", type: "tinyint(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.adminid);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "companys",
                columns: table => new
                {
                    companyid = table.Column<string>(name: "company_id", type: "varchar(255)", nullable: false),
                    companyname = table.Column<string>(name: "company_name", type: "varchar(255)", maxLength: 255, nullable: true),
                    Address = table.Column<string>(type: "longtext", nullable: true),
                    email = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    phone = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.companyid);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "contract_status",
                columns: table => new
                {
                    contractstatusid = table.Column<string>(name: "contract_status_id", type: "varchar(255)", nullable: false),
                    statusname = table.Column<string>(name: "status_name", type: "varchar(255)", maxLength: 255, nullable: true),
                    isDeleted = table.Column<bool>(type: "tinyint(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.contractstatusid);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "contract_types",
                columns: table => new
                {
                    contracttypeid = table.Column<string>(name: "contract_type_id", type: "varchar(255)", nullable: false),
                    typename = table.Column<string>(name: "type_name", type: "varchar(255)", maxLength: 255, nullable: true),
                    isDeleted = table.Column<bool>(type: "tinyint(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.contracttypeid);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "employees",
                columns: table => new
                {
                    employeeid = table.Column<string>(name: "employee_id", type: "varchar(255)", nullable: false),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    image = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    dateofbirth = table.Column<DateTime>(name: "date_of_birth", type: "date", nullable: true),
                    address = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    identifynumber = table.Column<int>(name: "identify_number", type: "int", nullable: true),
                    isActive = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    phonenumber = table.Column<string>(name: "phone_number", type: "varchar(20)", maxLength: 20, nullable: true),
                    code = table.Column<string>(type: "varchar(255)", nullable: true),
                    email = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.employeeid);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "holidays",
                columns: table => new
                {
                    holidayid = table.Column<string>(name: "holiday_id", type: "varchar(255)", nullable: false),
                    startdate = table.Column<DateTime>(name: "start_date", type: "date", nullable: true),
                    enddate = table.Column<DateTime>(name: "end_date", type: "date", nullable: true),
                    isDeleted = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    HolidayName = table.Column<string>(type: "longtext", nullable: true),
                    isPaid = table.Column<bool>(type: "tinyint(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.holidayid);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "salary_types",
                columns: table => new
                {
                    salarytypeid = table.Column<string>(name: "salary_type_id", type: "varchar(255)", nullable: false),
                    salarytypename = table.Column<string>(name: "salary_type_name", type: "varchar(255)", maxLength: 255, nullable: true),
                    isDeleted = table.Column<bool>(type: "tinyint(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.salarytypeid);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "contracts",
                columns: table => new
                {
                    contractid = table.Column<string>(name: "contract_id", type: "varchar(255)", nullable: false),
                    file = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    startdate = table.Column<DateTime>(name: "start_date", type: "date", nullable: true),
                    enddate = table.Column<DateTime>(name: "end_date", type: "date", nullable: true),
                    job = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    basicsalary = table.Column<double>(name: "basic_salary", type: "double", nullable: true),
                    bhxh = table.Column<double>(type: "double", nullable: true),
                    Tax = table.Column<double>(type: "double", nullable: true),
                    partnerid = table.Column<string>(name: "partner_id", type: "varchar(255)", nullable: false),
                    partnerprice = table.Column<double>(name: "partner_price", type: "double", nullable: true),
                    employeeid = table.Column<string>(name: "employee_id", type: "varchar(255)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "date", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "date", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "date", nullable: true),
                    Bhyt = table.Column<double>(type: "double", nullable: true),
                    Bhtn = table.Column<double>(type: "double", nullable: true),
                    SalaryType = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    ContractStatus = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    ContractType = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.contractid);
                    table.ForeignKey(
                        name: "contracts_ibfk_1",
                        column: x => x.employeeid,
                        principalTable: "employees",
                        principalColumn: "employee_id");
                    table.ForeignKey(
                        name: "contracts_ibfk_5",
                        column: x => x.partnerid,
                        principalTable: "companys",
                        principalColumn: "company_id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "leave_logs",
                columns: table => new
                {
                    leavetimeid = table.Column<string>(name: "leave_time_id", type: "varchar(255)", nullable: false),
                    startdate = table.Column<DateTime>(name: "start_date", type: "date", nullable: true),
                    enddate = table.Column<DateTime>(name: "end_date", type: "date", nullable: true),
                    reason = table.Column<string>(type: "text", nullable: true),
                    status = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    isDeleted = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    employeeid = table.Column<string>(name: "employee_id", type: "varchar(255)", nullable: true),
                    leavedate = table.Column<DateTime>(name: "leave_date", type: "date", nullable: true),
                    leavehours = table.Column<int>(name: "leave_hours", type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.leavetimeid);
                    table.ForeignKey(
                        name: "leave_logs_ibfk_1",
                        column: x => x.employeeid,
                        principalTable: "employees",
                        principalColumn: "employee_id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "overtime_logs",
                columns: table => new
                {
                    overtimeid = table.Column<string>(name: "overtime_id", type: "varchar(255)", nullable: false),
                    overtimeday = table.Column<DateTime>(name: "overtime_day", type: "date", nullable: true),
                    hours = table.Column<int>(type: "int", nullable: true),
                    status = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    isDeleted = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    employeeid = table.Column<string>(name: "employee_id", type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.overtimeid);
                    table.ForeignKey(
                        name: "overtime_logs_ibfk_1",
                        column: x => x.employeeid,
                        principalTable: "employees",
                        principalColumn: "employee_id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "payrolls",
                columns: table => new
                {
                    payrollid = table.Column<string>(name: "payroll_id", type: "varchar(255)", nullable: false),
                    total = table.Column<double>(type: "double", nullable: true),
                    tax = table.Column<double>(type: "double", nullable: true),
                    note = table.Column<string>(type: "text", nullable: true),
                    date = table.Column<DateTime>(type: "date", nullable: true),
                    isDeleted = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    employeeid = table.Column<string>(name: "employee_id", type: "varchar(255)", nullable: true),
                    totaldeduction = table.Column<double>(name: "total_deduction", type: "double", nullable: true),
                    totalbonus = table.Column<double>(name: "total_bonus", type: "double", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.payrollid);
                    table.ForeignKey(
                        name: "payrolls_ibfk_1",
                        column: x => x.employeeid,
                        principalTable: "employees",
                        principalColumn: "employee_id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "paid_history",
                columns: table => new
                {
                    payhistoryid = table.Column<string>(name: "pay_history_id", type: "varchar(255)", nullable: false),
                    employeeid = table.Column<string>(name: "employee_id", type: "varchar(255)", nullable: true),
                    contractid = table.Column<string>(name: "contract_id", type: "varchar(255)", nullable: true),
                    basesalary = table.Column<double>(name: "base_salary", type: "double", nullable: true),
                    workhours = table.Column<int>(name: "work_hours", type: "int", nullable: true),
                    othours = table.Column<int>(name: "ot_hours", type: "int", nullable: true),
                    leavehours = table.Column<int>(name: "leave_hours", type: "int", nullable: true),
                    socialinsurance = table.Column<double>(name: "social_insurance", type: "double", nullable: true),
                    accidentinsurance = table.Column<double>(name: "accident_insurance", type: "double", nullable: true),
                    healthinsurance = table.Column<double>(name: "health_insurance", type: "double", nullable: true),
                    paiddate = table.Column<DateTime>(name: "paid_date", type: "date", nullable: true),
                    salaryamount = table.Column<double>(name: "salary_amount", type: "double", nullable: true),
                    bonus = table.Column<double>(type: "double", nullable: true),
                    deductions = table.Column<double>(type: "double", nullable: true),
                    payrollperiodstart = table.Column<DateTime>(name: "payroll_period_start", type: "date", nullable: true),
                    payrollperiodend = table.Column<DateTime>(name: "payroll_period_end", type: "date", nullable: true),
                    createat = table.Column<DateTime>(name: "create_at", type: "date", nullable: true),
                    updateat = table.Column<DateTime>(name: "update_at", type: "date", nullable: true),
                    deletedat = table.Column<DateTime>(name: "deleted_at", type: "date", nullable: true),
                    note = table.Column<string>(type: "text", nullable: true),
                    paidtype = table.Column<string>(name: "paid_type", type: "varchar(255)", nullable: true),
                    tax = table.Column<double>(type: "double", nullable: true),
                    standardworkhours = table.Column<int>(name: "standard_work_hours", type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.payhistoryid);
                    table.ForeignKey(
                        name: "paid_history_ibfk_1",
                        column: x => x.employeeid,
                        principalTable: "employees",
                        principalColumn: "employee_id");
                    table.ForeignKey(
                        name: "paid_history_ibfk_2",
                        column: x => x.contractid,
                        principalTable: "contracts",
                        principalColumn: "contract_id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "employee_id",
                table: "contracts",
                column: "employee_id");

            migrationBuilder.CreateIndex(
                name: "partner_id",
                table: "contracts",
                column: "partner_id");

            migrationBuilder.CreateIndex(
                name: "code",
                table: "employees",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "employee_id1",
                table: "leave_logs",
                column: "employee_id");

            migrationBuilder.CreateIndex(
                name: "employee_id2",
                table: "overtime_logs",
                column: "employee_id");

            migrationBuilder.CreateIndex(
                name: "contract_id",
                table: "paid_history",
                column: "contract_id");

            migrationBuilder.CreateIndex(
                name: "employee_id3",
                table: "paid_history",
                column: "employee_id");

            migrationBuilder.CreateIndex(
                name: "optimize_query",
                table: "paid_history",
                columns: new[] { "paid_date", "paid_type", "deleted_at" });

            migrationBuilder.CreateIndex(
                name: "employee_id4",
                table: "payrolls",
                column: "employee_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "admins");

            migrationBuilder.DropTable(
                name: "contract_status");

            migrationBuilder.DropTable(
                name: "contract_types");

            migrationBuilder.DropTable(
                name: "holidays");

            migrationBuilder.DropTable(
                name: "leave_logs");

            migrationBuilder.DropTable(
                name: "overtime_logs");

            migrationBuilder.DropTable(
                name: "paid_history");

            migrationBuilder.DropTable(
                name: "payrolls");

            migrationBuilder.DropTable(
                name: "salary_types");

            migrationBuilder.DropTable(
                name: "contracts");

            migrationBuilder.DropTable(
                name: "employees");

            migrationBuilder.DropTable(
                name: "companys");
        }
    }
}
