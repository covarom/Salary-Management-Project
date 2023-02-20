using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryManagement.Contracts.Payroll
{
    public record PayrollResponse
    {
        public string PayrollId { get; init; }
        public DateTime? FromDate { get; init; }
        public DateTime? ToDate { get; init; }
        public double? GrossSalary { get; init; }
        public double? NetSalary { get; init; }
        public bool? Paid { get; init; }
    }
}
