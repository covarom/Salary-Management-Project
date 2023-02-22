using Mapster;
using SalaryManagement.Application.Common.Interfaces.Persistence;
using SalaryManagement.Contracts.Companys;
using SalaryManagement.Contracts.Contracts;
using SalaryManagement.Contracts.Employees;
using SalaryManagement.Contracts.Salary;
using SalaryManagement.Domain.Common.Enum;
using SalaryManagement.Domain.Entities;

namespace SalaryManagement.Application.Services.SalaryServices
{
    public class SalaryService : ISalaryService
    {

        private readonly IContractRepository _repository;
        private readonly ICompanyRepository _companyRepository;

        public SalaryService(IContractRepository repository, ICompanyRepository companyRepository) 
        { 
            _repository = repository;
            _companyRepository = companyRepository;
        }

        public async Task<SalaryResponse> CalculateSalaryAsync(Employee employee, decimal? otTime, int? leaveTime)
        {

            decimal salary = 0;

            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }

            var contract = await _repository.GetContractsByEmployeeIdAsync(employee.EmployeeId);

            if (contract == null)
            {
                // return salary;
                return new SalaryResponse
                {
                    Employee = employee.Adapt<EmployeeResponse>(),
                    Company = contract.Partner.Adapt<CompanyResponse>(),
                    StandardWorkHours = 30 * 8,
                    RealityWorkHours = 30 * 8 + (decimal)otTime - (decimal)leaveTime * 8,
                    BaseSalary = (double)contract.BasicSalary,
                    BaseSalaryPerHour = (double)contract.BasicSalary / (30 * 8),
                    Tax = 0,
                    SocialInsurance = 0,
                    AccidentInsurance = 0,
                    HealthInsurance = 0,
                    OvertimeHours = (decimal)otTime,
                    OvetimeSalaryPerHour = (double)contract.BasicSalary / (30 * 8) * 1.5,
                    TotalBonus = 0,
                    TotalDeductions = 0,
                    LeaveHours = (decimal)leaveTime * 8
                };
            }

            var company = _companyRepository.GetById(contract.PartnerId);

            decimal basicSalary = (decimal)contract.BasicSalary;
            decimal bhxh = (decimal)contract.Bhxh;
            decimal bhyt = (decimal)contract.Bhyt;
            decimal bhtn = (decimal)contract.Bhtn;
            decimal personalIncomeTax = (decimal)contract.Tax;

            decimal EarnedPerHour = basicSalary / 30 / 8;
            int workDay = GetTotalWorkingDate();
            decimal tempSalary = EarnedPerHour * workDay;

            if (contract.SalaryType == SalaryTypeEnum.Gross.ToString())
            {
                // Calculate gross salary
                decimal totalDeductions = personalIncomeTax + bhxh + bhyt + bhtn;
                salary = tempSalary - totalDeductions;
            }
            else if (contract.SalaryType == SalaryTypeEnum.Net.ToString())
            {
                // Calculate net salary
                salary = tempSalary;
            }

            // Calculate overtime pay
            decimal overtimePay = 0;
            if (otTime != null && otTime > 0)
            {
                decimal hourlyRate = basicSalary / 30 / 8;
                overtimePay = (decimal)(otTime * hourlyRate * (decimal)1.5);
            }

            // Calculate leave deduction
            //Hien tai dang nghi theo ngay
            decimal leaveDeduction = 0;
            if (leaveTime != null && leaveTime > 0)
            {
                decimal hourlyRate = basicSalary / 30 / 8;
                leaveDeduction = (decimal)leaveTime * 8 * hourlyRate;
            }

            salary += overtimePay - leaveDeduction;

            return new SalaryResponse
            {
                Employee = employee.Adapt<EmployeeResponse>(),
                Company = company.Result.Adapt<CompanyResponse>(),
                StandardWorkHours = 30 * 8,
                RealityWorkHours = GetTotalWorkingDate() * 8 + (decimal)otTime - (decimal)leaveTime * 8,
                BaseSalary = (double)contract.BasicSalary,
                BaseSalaryPerHour = (double)contract.BasicSalary / (30 * 8),
                Tax = personalIncomeTax,
                SocialInsurance = bhxh,
                AccidentInsurance = bhtn,
                HealthInsurance = bhyt,
                OvertimeHours = (decimal)otTime,
                OvetimeSalaryPerHour = (double)contract.BasicSalary / (30 * 8) * 1.5 ,
                TotalBonus = (double)overtimePay,
                TotalDeductions = (double)leaveDeduction,
                LeaveHours = (decimal)leaveTime*8,
                FinalIncome = salary
            };
        }

        private int GetTotalWorkingDate()
        {
            DateTime startDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            int totalDays = (DateTime.Today - startDate).Days + 1;  // add 1 to include today
            return Enumerable.Range(0, totalDays)
                .Select(d => startDate.AddDays(d))
                .Count(date => date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday);
            
        }

    }
}
