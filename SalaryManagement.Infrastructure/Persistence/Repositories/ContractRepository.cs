using Microsoft.EntityFrameworkCore;
using SalaryManagement.Application.Common.Interfaces.Persistence;
using SalaryManagement.Contracts;
using SalaryManagement.Domain.Entities;

namespace SalaryManagement.Infrastructure.Persistence.Repositories
{
    public class ContractRepository : IContractRepository
    {
        private readonly SalaryManagementContext _context;

        public ContractRepository(SalaryManagementContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Contract>> GetAllContractsAsync()
        {
            return await _context.Contracts.ToListAsync();
        }

        public async Task<Contract> AddContractAsync(Contract contract)
        {
            _context.Contracts.Add(contract);
            await _context.SaveChangesAsync();
            return contract;
        }

        public async Task UpdateContractAsync(Contract contract)
        {
            _context.Contracts.Update(contract);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteContractAsync(int id)
        {
            var contract = await _context.Contracts.FindAsync(id);
            _context.Contracts.Remove(contract);
            await _context.SaveChangesAsync();
        }

        public async Task<Contract?> GetContractByIdAsync(string id)
        {
            return await _context.Contracts.Include(x => x.Employee)
                .Include(y => y.ContractStatus )
                .Include(z => z.ContractType )
                .Include(k => k.SalaryType)
                .FirstOrDefaultAsync(c => c.ContractId == id);
        }

        public async Task DeleteContractAsync(Contract contract)
        {
            _context.Contracts.Remove(contract);
            await _context.SaveChangesAsync();
        }

        public async Task<PaginatedResponse<Contract>> GetContractsAsync(int pageNumber, int pageSize, string? searchKeyword, string? sortBy, bool? isDesc)
        {
            var query = _context.Contracts
                .Include(c => c.Employee)
                .Include(c => c.ContractStatus)
                .Include(c => c.ContractType)
                .Include(c => c.SalaryType)
                .Include(c => c.Partner)
                .AsQueryable();

            // Search contracts by keyword
            if (!string.IsNullOrEmpty(searchKeyword))
            {
                query = query.Where(c => c.ContractId.Contains(searchKeyword) 
                || c.Employee.Name.Contains(searchKeyword) 
                || c.ContractType.TypeName.Contains(searchKeyword) 
                || c.Partner.CompanyName.Contains(searchKeyword) 
                || c.SalaryType.SalaryTypeName.Contains(searchKeyword) 
                || c.ContractStatus.StatusName.Contains(searchKeyword));
            }

            // Sort contracts
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "id":
                        query = (isDesc == true) ? query.OrderByDescending(c => c.ContractId) : query.OrderBy(c => c.ContractId);
                        break;
                    case "startDate":
                         query = (isDesc == true) ? query.OrderByDescending(c => c.StartDate): query.OrderBy(c => c.StartDate);
                        break;
                    case "endDate":
                        query = (isDesc == true) ? query.OrderByDescending(c => c.EndDate) : query.OrderBy(c => c.EndDate);
                        break;
                    default:
                        break;
                }
            }

            var totalCount = await query.CountAsync();

            // Calculate the current page and total page based on page size and total count
            var currentPage = pageNumber;
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
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
                       
            var paginatedQuery =  query.Skip(skipRow).Take(pageSize);
            var results = await paginatedQuery.ToListAsync();

            return new PaginatedResponse<Contract>
            {
                CurrentPage = currentPage,
                TotalPages = totalPages,
                IntemPerPage = pageSize,
                TotalCount = totalCount,
                Results = results
            };
        }

      
    }
}

