using Mapster;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
using SalaryManagement.Application.Common.Exception;
using SalaryManagement.Application.Common.Interfaces.Persistence;
using SalaryManagement.Contracts;
using SalaryManagement.Contracts.Dashboards;
using SalaryManagement.Contracts.PaidHistory;
using SalaryManagement.Domain.Common.Enum;
using SalaryManagement.Domain.Entities;
using System.Linq.Dynamic.Core;

namespace SalaryManagement.Infrastructure.Persistence.Repositories
{
    public class PaidHistoryRepository : IPaidHistoryRepository
    {
        private readonly SalaryManagementContext _context;

        public PaidHistoryRepository(SalaryManagementContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<PaidHistory> CreateAsync(PaidHistory paidHistory)
        {
            await _context.PaidHistories.AddAsync(paidHistory);
            await _context.SaveChangesAsync();
            return paidHistory;
        }

        //GET
        public async Task<PaginatedResponse<PaidHistoryResponse>> GetPaidHistoriesAsync(int pageNumber, int pageSize, string? sortBy, bool isDesc, string? searchKeyword)
        {
            var query = _context.PaidHistories
                .Include(x => x.Employee)
                .Include(x => x.Contract)
                .Include(x => x.Contract.Partner)
                .AsQueryable();

            //Not get the deleted one
            query = query.Where(c => c.DeletedAt == null);

            // apply search filter if searchKeyword is not null or empty
            if (!string.IsNullOrEmpty(searchKeyword))
            {
                query = query.Where(c => c.Employee.Name.Contains(searchKeyword));
            }

            // apply sorting if sortBy is not null or empty
            if (!string.IsNullOrEmpty(sortBy))
            {
                query = query.OrderBy(sortBy, isDesc);
            }

            var totalItems = await query.CountAsync();

            if (pageNumber < 1) pageNumber = 1;

            var currentPage = pageNumber;
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            totalPages = totalPages > 0 ? totalPages : 0;

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

            var response = new PaginatedResponse<PaidHistoryResponse>
            {
                Results = results.Adapt<List<PaidHistoryResponse>>(),
                TotalCount = totalItems,
                CurrentPage = pageNumber,
                ItemPerPage = pageSize,
                TotalPages = totalPages
            };

            return response;
        }

        public async Task<PaidHistory> GetByIdAsync(string id)
        {
            return await _context.PaidHistories
                .Include(x => x.Employee)
                .Include(x => x.Contract)
                .Include(x => x.Contract.Partner)
                .FirstOrDefaultAsync(x => x.DeletedAt == null && x.PayHistoryId.Equals(id));
        }

        public async Task UpdatePaidHistoryAsync(PaidHistory paidHistory)
        {
            _context.Entry(paidHistory).State = EntityState.Detached;
            _context.Update(paidHistory);
            await _context.SaveChangesAsync();
        }
        public async Task<int> CountPaySlipsActive()
        {
            var num = await _context.PaidHistories.Where(x => x.DeletedAt == null).CountAsync();
            return num;
        }

        public async Task<int> CountPaySlipByType(string type)
        {
            return await _context.PaidHistories.Where(x => x.DeletedAt == null && x.PaidType.Equals(type)).CountAsync();
        }

        public async Task<RevenueCostChartResponse> RevenueCostByDate(DateTime date)
        {
            double revenue = (double)_context.PaidHistories.Where(x => x.DeletedAt == null
                && ((DateTime)x.PaidDate).Month == date.Month
                && ((DateTime)x.PaidDate).Year== date.Year
                && x.PaidType.Equals(SalaryCaculatingType.Staff.ToString()))
                .Sum(x => x.SalaryAmount);

            double cost = (double)_context.PaidHistories.Where(x => x.DeletedAt == null
               && ((DateTime)x.PaidDate).Month == date.Month
               && ((DateTime)x.PaidDate).Year == date.Year
               && x.PaidType.Equals(SalaryCaculatingType.Partner.ToString()))
               .Sum(x => x.SalaryAmount);

            var dateResponse = date.Month + "/" + date.Year;
            return new RevenueCostChartResponse
            {
                Cost = Math.Round(cost,2),
                Revenue = Math.Round(revenue,2),
                Date = dateResponse
            };
        }
    }
}
