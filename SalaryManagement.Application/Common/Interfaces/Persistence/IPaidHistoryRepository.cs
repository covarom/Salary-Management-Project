using SalaryManagement.Contracts;
using SalaryManagement.Contracts.Dashboards;
using SalaryManagement.Contracts.PaidHistory;
using SalaryManagement.Domain.Entities;

namespace SalaryManagement.Application.Common.Interfaces.Persistence
{
    public interface IPaidHistoryRepository
    {
        Task<PaginatedResponse<PaidHistoryResponse>> GetPaidHistoriesAsync(int pageNumber, int pageSize, string? sortBy, bool isDesc, string? searchKeyword);

        Task<PaidHistory> CreateAsync(PaidHistory paidHistory);

        Task<PaidHistory> GetByIdAsync(string id);

        Task UpdatePaidHistoryAsync(PaidHistory paidHistory);

        Task<int> CountPaySlipsActive();

        Task<int> CountPaySlipByType(string type);

        Task<RevenueCostChartResponse> RevenueCostByDate(DateTime date);

    }
}
