using Microsoft.EntityFrameworkCore;
using SalaryManagement.Application.Common.Interfaces.Persistence;
using SalaryManagement.Contracts;
using SalaryManagement.Contracts.Contracts;
using SalaryManagement.Domain.Entities;
using System.Linq.Dynamic.Core;
using Mapster;
using SalaryManagement.Domain.Common.Enum;

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

        public async Task AddAsync(Contract contract)
        {
            await _context.Contracts.AddAsync(contract);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateContract(Contract contractToUpdate)
        {
            _context.Entry(contractToUpdate).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        public async Task<Contract?> GetContractById(string contractId)
        {
            return await _context.Contracts.FirstOrDefaultAsync(c => c.ContractId.Equals(contractId) && (c.DeletedAt == null));
        } 

        public async Task<bool> DeleteContractAsync(Contract contract)
        {
            try
            {
                contract.ContractStatus = ContractStatusEnum.Terminated.ToString();
                contract.DeletedAt = DateTime.UtcNow;

                // Update the contract in the database
                _context.Contracts.Update(contract);
                await _context.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
            
        }

        public async Task<ContractResponse?> GetContractByIdAsync(string id)
        {
            var contract =  await _context.Contracts.Include(x => x.Employee)
                .Include(x => x.Partner)
                .FirstOrDefaultAsync(c => c.ContractId == id && c.DeletedAt == null);

            if (contract == null) return null;

            return contract.Adapt<ContractResponse>();
        }

        public async Task<bool> DeleteContractAsync(string contractId)
        {
             var contract = await _context.Contracts.FirstOrDefaultAsync(c => c.ContractId == contractId && c.DeletedAt == null);


            if (contract != null)
            {
                contract.DeletedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }

        /*public async Task<PaginatedResponse<Contract>> GetContractsAsync(int pageNumber, int pageSize, string? searchKeyword, string? sortBy, bool? isDesc)
        {

         //   var contracts = _context.Contracts.ToList();
            var query = _context.Contracts
                .Include(c => c.Employee)
                .Include(c => c.Partner)
                .AsQueryable();

            // Search contracts by keyword
            if (!string.IsNullOrEmpty(searchKeyword))
            {
                query = query.Where(c => c.ContractId.Contains(searchKeyword)
                || c.Employee.Name.Contains(searchKeyword)
                || c.Partner.CompanyName.Contains(searchKeyword));
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
                        query = (isDesc == true) ? query.OrderByDescending(c => c.StartDate) : query.OrderBy(c => c.StartDate);
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
            if (pageNumber < 1) pageNumber = 1;
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

            var paginatedQuery = query.Skip(skipRow).Take(pageSize);
            var results = await paginatedQuery.ToListAsync();

            return new PaginatedResponse<Contract>
            {
                CurrentPage = currentPage,
                TotalPages = totalPages,
                ItemPerPage = pageSize,
                TotalCount = totalCount,
                Results = results
            };

        }*/

        private bool ContractExists(string id)
        {
            return _context.Contracts.Any(e => e.ContractId == id);
        }


        public async Task<PaginatedResponse<ContractResponse>> GetAllContracts(int pageNumber, int pageSize, string? sortBy, bool isDesc, string? searchKeyword)
        {
            var query = _context.Contracts.
                Select(c => new Contract
                {
                    ContractId = c.ContractId,
                    Job = c.Job,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,
                    BasicSalary= c.BasicSalary,
                    Bhtn = c.Bhtn,
                    Bhxh= c.Bhxh,
                    Bhyt= c.Bhyt,
                    ContractStatus= c.ContractStatus,
                    ContractType= c.ContractType,
                    CreatedAt= c.CreatedAt,
                    DeletedAt= c.DeletedAt,
                    EmployeeId= c.EmployeeId,
                    File = c.File,
                    PartnerId= c.PartnerId,
                    PartnerPrice= c.PartnerPrice,
                    SalaryType= c.SalaryType,
                    Tax = c.Tax,
                    UpdatedAt = c.UpdatedAt,
                    Employee = c.Employee,
                    Partner = c.Partner
                })
                .AsQueryable();

            //Not get the deleted one
            query = query.Where(c => c.DeletedAt == null);

            // apply search filter if searchKeyword is not null or empty
            if (!string.IsNullOrEmpty(searchKeyword))
            {
                query = query.Where(c => c.Job.Contains(searchKeyword) || (c.Employee != null && c.Employee.Name.Contains(searchKeyword)));
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

            var response = new PaginatedResponse<ContractResponse>
            {
                Results = results.Adapt<List<ContractResponse>>(),
                TotalCount = totalItems,
                CurrentPage = pageNumber,
                ItemPerPage= pageSize,
                TotalPages = totalPages
            };

            return response;
        }
          public async Task<Contract> GetContractByCompanyId(string id)
        {
            // var contract =  _context.Contracts.AnyAsync(c => c.PartnerId == id && c.DeletedAt == null);

            var contract = await _context.Contracts.Include(x => x.Partner).FirstOrDefaultAsync(x => x.PartnerId == id);

            if (contract == null) return null;

            return contract;
        }

      
        
    }

}

