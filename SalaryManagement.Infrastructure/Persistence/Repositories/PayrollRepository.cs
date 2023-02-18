using Microsoft.EntityFrameworkCore;
using SalaryManagement.Application.Common.Interfaces.Persistence;
using SalaryManagement.Contracts;
using SalaryManagement.Domain.Entities;

namespace SalaryManagement.Infrastructure.Persistence.Repositories
{
    public class PayrollRepository : IPayrollRepository
    {
        private readonly SalaryManagementContext _context;

        public PayrollRepository(SalaryManagementContext context)
        {
            _context = context;
        }

        public async Task<Payroll> AddPayroll(Payroll payroll)
        {
            _context.Payrolls.Add(payroll);
            await _context.SaveChangesAsync();
            return payroll;
        }

        public async Task<bool> DeletePayroll(string id)
        {
            bool check = false;
            var payroll = await _context.Payrolls.FindAsync(id);
            _context.Payrolls.Remove(payroll);
            int change = await _context.SaveChangesAsync();

            if (change > 0)
            {
                check = true;
            }

            return check;
        }

        public async Task<Payroll> GetById(string id)
        {
            //var query = _context.Payrolls.Include(e => e.Employee).AsQueryable();
            return await _context.Payrolls.Include(e => e.Employee).FirstOrDefaultAsync(x => x.PayrollId.Equals(id));
        }

        public async Task<PaginatedResponse<Payroll>> GetAll(int pageNumber, int pageSize, string? keyword, string? sortBy, bool? isDesc)
        {
            var query =  _context.Payrolls.Include(e => e.Employee).Select(p => new Payroll
            {
                PayrollId = p.PayrollId,
                Total = p.Total,
                Tax = p.Tax,
                Note = p.Note,
                Date = p.Date,
                IsDeleted = p.IsDeleted,
                EmployeeId = p.EmployeeId,
                Employee = p.Employee
            }).AsQueryable();

            if(!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(p => p.PayrollId.Contains(keyword)
                || p.Employee.Name.Contains(keyword)
                || p.Date.ToString().Contains(keyword));
            }

            if(!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "total":
                        query = (isDesc == true) ? query.OrderByDescending(p => p.Total) : query.OrderBy(p => p.Total);
                        break;
                    case "date":
                        query = (isDesc == true) ? query.OrderByDescending(p => p.Date) : query.OrderBy(p => p.Date);
                        break;
                    case "name":
                        query = (isDesc == true) ? query.OrderByDescending(p => p.Employee.Name) : query.OrderBy(p => p.Employee.Name);
                        break;
                    default:
                        break;
                }
            }

            var totalCount = await query.CountAsync();
            var currentPage = pageNumber;
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            totalPages = totalPages > 0 ? totalPages: 0;

            if (currentPage < 1)
            {
                currentPage = 1;
            }
            else if (currentPage > totalPages)
            {
                currentPage = totalPages;
            }

            var skipRow = (currentPage - 1) * pageSize;

            skipRow = (skipRow >= 0) ? skipRow : 0;

            var paginatedQuery = query.Skip(skipRow).Take(pageSize);
            var results = await paginatedQuery.ToListAsync();

            return new PaginatedResponse<Payroll>
            {
                CurrentPage = currentPage,
                TotalPages = totalPages,
                ItemPerPage = pageSize,
                TotalCount = totalCount,
                Results = results
            };
        }

        public async Task<bool> UpdatePayroll(Payroll payroll)
        {
            bool check = false;
            _context.Payrolls.Update(payroll);
            int change = await _context.SaveChangesAsync();

            if (change > 0)
            {
                check = true;
            }

            return check;
        }
    }
}
