using Mapster;
using MapsterMapper;
using SalaryManagement.Application.Common.Exception;
using SalaryManagement.Application.Common.Interfaces.Persistence;
using SalaryManagement.Contracts;
using SalaryManagement.Contracts.PaidHistory;
using SalaryManagement.Domain.Entities;

namespace SalaryManagement.Application.Services.PaidHistoryServices
{
    public class PaidHistoryService : IPaidHistoryService
    {
        private readonly IPaidHistoryRepository _paidHistoryRepository;
        private readonly IMapper _mapper;

        public PaidHistoryService(IPaidHistoryRepository paidHistoryRepository, IMapper mapper)
        {
            _paidHistoryRepository = paidHistoryRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedResponse<PaidHistoryResponse>> GetPaidHistories(int pageNumber, int pageSize, string? sortBy, bool isDesc, string? searchKeyword)
        {
            return await _paidHistoryRepository.GetPaidHistoriesAsync(pageNumber, pageSize, sortBy, isDesc, searchKeyword);
        }

        public async Task<PaidHistory> SavePaidHistory(PaidHistoryRequest paidHistoryRequest)
        {
            // Map request to entity
            var paidHistory = _mapper.Map<PaidHistory>(paidHistoryRequest);

            // Set additional fields as needed
            paidHistory.PayHistoryId = Guid.NewGuid().ToString();
            paidHistory.CreateAt = DateTime.Now;

            // Save entity using repository
            await _paidHistoryRepository.CreateAsync(paidHistory);

            // Return saved entity
            return paidHistory;
        }

        public async Task<PaidHistoryResponse> GetByIdAsync(string id)
        {
            var paidHistory = await _paidHistoryRepository.GetByIdAsync(id);
            if (paidHistory == null)
            {
                return null;
            }
            return paidHistory.Adapt<PaidHistoryResponse>();
        }

        public async Task<PaidHistory> UpdatePaidHistoryAsync(string id, PaidHistoryRequest paidHistoryRequest)
        {
            var paidHistory = await _paidHistoryRepository.GetByIdAsync(id);

            if (paidHistory == null)
            {
                throw new NotFoundException($"PaidHistory with ID {id} not found");
            }

            // Update the fields of the paid history with the new values
            paidHistory.EmployeeId = paidHistoryRequest.EmployeeId;
            paidHistory.ContractId = paidHistoryRequest.ContractId;
            paidHistory.BaseSalary = paidHistoryRequest.BaseSalary;
            paidHistory.WorkHours = paidHistoryRequest.WorkHours;
            paidHistory.OtHours = paidHistoryRequest.OtHours;
            paidHistory.LeaveHours = paidHistoryRequest.LeaveHours;
            paidHistory.SocialInsurance = paidHistoryRequest.SocialInsurance;
            paidHistory.AccidentInsurance = paidHistoryRequest.AccidentInsurance;
            paidHistory.HealthInsurance = paidHistoryRequest.HealthInsurance;
            paidHistory.PaidDate = paidHistoryRequest.PaidDate;
            paidHistory.SalaryAmount = paidHistoryRequest.SalaryAmount;
            paidHistory.Bonus = paidHistoryRequest.Bonus;
            paidHistory.Deductions = paidHistoryRequest.Deductions;
            paidHistory.PayrollPeriodStart = paidHistoryRequest.PayrollPeriodStart;
            paidHistory.PayrollPeriodEnd = paidHistoryRequest.PayrollPeriodEnd;
            paidHistory.Note = paidHistoryRequest.Note;
            paidHistory.UpdateAt = DateTime.Now;

            await _paidHistoryRepository.UpdatePaidHistoryAsync(paidHistory);

            return paidHistory;
        }

        public async Task DeleteAsync(string id)
        {
            var paidHistory = await _paidHistoryRepository.GetByIdAsync(id);

            if (paidHistory == null)
            {
                throw new NotFoundException($"Paid history with id {id} not found.");
            }

            paidHistory.DeletedAt = DateTime.UtcNow;

            await _paidHistoryRepository.UpdatePaidHistoryAsync(paidHistory);
        }
        public async Task<int> CountPaySlipsActive()
        {
            return await _paidHistoryRepository.CountPaySlipsActive();
        }

    }
}
