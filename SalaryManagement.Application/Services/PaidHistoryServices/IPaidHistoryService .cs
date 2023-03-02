using SalaryManagement.Contracts;
using SalaryManagement.Contracts.PaidHistory;
using SalaryManagement.Domain.Entities;

namespace SalaryManagement.Application.Services.PaidHistoryServices
{
    public interface IPaidHistoryService
    {
        Task<PaginatedResponse<PaidHistoryResponse>> GetPaidHistories(int pageNumber, int pageSize, string? sortBy, bool isDesc, string? searchKeyword);

        Task<PaidHistory> SavePaidHistory(PaidHistoryRequest paidHistoryRequest);

        Task<PaidHistoryResponse> GetByIdAsync(string id);

        Task<PaidHistory> UpdatePaidHistoryAsync(string id, PaidHistoryRequest paidHistoryRequest);

        Task DeleteAsync(string id);
    }
}
