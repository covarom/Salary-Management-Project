﻿using Microsoft.EntityFrameworkCore;
using SalaryManagement.Application.Common.Interfaces.Persistence;
using SalaryManagement.Domain.Entities;

namespace SalaryManagement.Infrastructure.Persistence.Repositories
{
    public class OvertimeRepository : IOvertimeRepository
    {
        private readonly SalaryManagementContext _context;

        public OvertimeRepository(SalaryManagementContext context)
        {
            _context = context;
        }

        public async Task<int> GetTotalOvertimeHoursByEmployeeIdAsync(string employeeId)
        {
            var startDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            var endDAte = startDate.AddMonths(1).AddDays(-1);
            var overtimeEntries = await _context.OvertimeLogs
                .Where(o => o.EmployeeId == employeeId && o.OvertimeDay >= startDate && o.OvertimeDay <= endDAte)
                .ToListAsync();

            var totalOvertimeHours = (int)overtimeEntries.Sum(o => o.Hours);

            return totalOvertimeHours;
        }

    }
}
