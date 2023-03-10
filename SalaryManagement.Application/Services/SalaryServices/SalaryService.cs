using Mapster;
using SalaryManagement.Application.Common.Interfaces.Persistence;

using SalaryManagement.Contracts.Contracts;

using SalaryManagement.Contracts.Salary;
using SalaryManagement.Domain.Common.Enum;
using SalaryManagement.Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SalaryManagement.Application.Services.SalaryServices
{
    public class SalaryService : ISalaryService
    {

        private readonly IContractRepository _repository;
        private readonly ICompanyRepository _companyRepository;

        private readonly IHolidayRepository _holidayRepository;

        public SalaryService(IContractRepository repository, ICompanyRepository companyRepository, IHolidayRepository holidayRepository) 
        { 
            _repository = repository;
            _companyRepository = companyRepository;
            _holidayRepository = holidayRepository;

        }

        public async Task<SalaryResponse> CalculateSalaryAsync(Employee employee, int? otTime, int? leaveTime, DateTime date)
        {

            decimal salary = 0;


            var contract = await _repository.GetContractsByEmployeeIdAsync(employee.EmployeeId);

            int standardWorkingHours;
            int realityWorkHours;
            int realityWorkingDays= await GetTotalWorkingDaysInMonth(date, (DateTime)contract.StartDate);
            int standardWorkingDays = await GetTotalWorkingDateOfAMonth(date); 

            if (contract.ContractType.Equals(ContractTypeEnum.PartTime.ToString()))
            {
                standardWorkingHours = standardWorkingDays * 4;
                realityWorkHours = realityWorkingDays * 4;
            }
            else
            {
                standardWorkingHours = standardWorkingDays * 8;
                realityWorkHours = realityWorkingDays * 8;
            }

            decimal basicSalary = (decimal)contract.BasicSalary;
            decimal bhxh = (decimal)contract.Bhxh;
            decimal bhyt = (decimal)contract.Bhyt;
            decimal bhtn = (decimal)contract.Bhtn;
            decimal personalIncomeTax = (decimal)contract.Tax;
            decimal EarnedPerHour = basicSalary / standardWorkingHours;            
            decimal tempSalary = EarnedPerHour * realityWorkHours;

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
                decimal hourlyRate = basicSalary / standardWorkingHours;
                overtimePay = (decimal)(otTime * hourlyRate * (decimal)1.5);
                realityWorkHours += (int)otTime;
            }

            // Calculate leave deduction
            //Hien tai dang nghi theo ngay
            decimal leaveDeduction = 0;
            if (leaveTime != null && leaveTime > 0)
            {
                decimal hourlyRate = basicSalary / standardWorkingHours;
                leaveDeduction = (decimal)leaveTime * hourlyRate;
                realityWorkHours -= (int)leaveTime;
            }

            salary += overtimePay - leaveDeduction;

            var startDate = new DateTime(date.Year, date.Month, 1);
            if (((DateTime)contract.StartDate).Month == date.Month && ((DateTime)contract.StartDate).Year == date.Year)
            {
                startDate = (DateTime)contract.StartDate;
            }

            return new SalaryResponse
            {
                Contract = contract.Adapt<ContractResponse>(),
                StandardWorkHours = standardWorkingHours,
                RealityWorkHours = realityWorkHours/* + (int)otTime - ((int)leaveTime * 8)*/,
                BaseSalary = Math.Round((double)contract.BasicSalary, 2),
                BaseSalaryPerHour = Math.Round((double)EarnedPerHour, 2),
                Tax = contract.SalaryType == SalaryTypeEnum.Gross.ToString() ? personalIncomeTax : 0,
                SocialInsurance = contract.SalaryType == SalaryTypeEnum.Gross.ToString() ? bhxh : 0,
                AccidentInsurance = contract.SalaryType == SalaryTypeEnum.Gross.ToString() ? bhtn : 0,
                HealthInsurance = contract.SalaryType == SalaryTypeEnum.Gross.ToString() ? bhyt : 0,
                OvertimeHours = (int)otTime,
                OvetimeSalaryPerHour = Math.Round((double)EarnedPerHour * 1.5, 2 ) ,
                TotalBonus = Math.Round((double)overtimePay,2),
                TotalDeductions = Math.Round((double)leaveDeduction,2),
                LeaveHours = (int)leaveTime,
                PeriodStartDate = startDate,
                PeriodEndDate = startDate.AddMonths(1).AddDays(-startDate.Day),
                FinalIncome =  Math.Round(salary, 2)
            };
        }

        public async Task<SalaryResponse> CalculateSalaryForPartnerAsync(Employee employee, int? otTime, int? leaveTime, DateTime date)
        {

            decimal salary = 0;

            var contract = await _repository.GetContractsByEmployeeIdAsync(employee.EmployeeId);

            int standardWorkingHours;
            int realityWorkHours;
            int realityWorkingDays = await GetTotalWorkingDaysInMonth(date, (DateTime)contract.StartDate);
            int standardWorkingDays = await GetTotalWorkingDateOfAMonth(date);

            if (contract.ContractType.Equals(ContractTypeEnum.PartTime.ToString()))
            {
                standardWorkingHours = standardWorkingDays * 4;
                realityWorkHours = realityWorkingDays * 4;
            }
            else
            {
                standardWorkingHours = standardWorkingDays * 8;
                realityWorkHours = realityWorkingDays * 8;
            }

            decimal basicSalary = (decimal)contract.PartnerPrice;
            /*decimal bhxh = (decimal)contract.Bhxh;
            decimal bhyt = (decimal)contract.Bhyt;
            decimal bhtn = (decimal)contract.Bhtn;
            decimal personalIncomeTax = (decimal)contract.Tax;*/
            decimal EarnedPerHour = basicSalary / standardWorkingHours;
            decimal tempSalary = EarnedPerHour * realityWorkHours;

            if (contract.SalaryType == SalaryTypeEnum.Gross.ToString())
            {
                // Calculate gross salary
                /* decimal totalDeductions = personalIncomeTax + bhxh + bhyt + bhtn;
                 salary = tempSalary - totalDeductions;*/
                salary = tempSalary;
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
                decimal hourlyRate = basicSalary / standardWorkingHours;
                overtimePay = (decimal)(otTime * hourlyRate * (decimal)1.5);
                realityWorkHours += (int)otTime;
            }

            // Calculate leave deduction
            //Hien tai dang nghi theo ngay
            decimal leaveDeduction = 0;
            if (leaveTime != null && leaveTime > 0)
            {
                decimal hourlyRate = basicSalary / standardWorkingHours;
                leaveDeduction = (decimal)leaveTime * hourlyRate;
                realityWorkHours -= (int)leaveTime;
            }

            salary += overtimePay - leaveDeduction;

            var startDate = new DateTime(date.Year, date.Month, 1);

            if (((DateTime)contract.StartDate).Month == date.Month && ((DateTime)contract.StartDate).Year == date.Year)
            {
                startDate = (DateTime)contract.StartDate;
            }

            return new SalaryResponse
            {
                Contract = contract.Adapt<ContractResponse>(),
                StandardWorkHours = standardWorkingHours,
                RealityWorkHours = realityWorkHours/* + (int)otTime - ((int)leaveTime * 8)*/,
                BaseSalary = Math.Round((double)contract.PartnerPrice, 2),
                BaseSalaryPerHour = Math.Round((double)EarnedPerHour, 2),
                Tax = 0,
                SocialInsurance = 0,
                AccidentInsurance = 0,
                HealthInsurance = 0,
                OvertimeHours = (int)otTime,
                OvetimeSalaryPerHour = Math.Round((double)EarnedPerHour * 1.5, 2),
                TotalBonus = Math.Round((double)overtimePay, 2),
                TotalDeductions = Math.Round((double)leaveDeduction, 2),
                LeaveHours = (int)leaveTime,
                PeriodStartDate = startDate,
                PeriodEndDate = startDate.AddMonths(1).AddDays(-startDate.Day + 1),
                FinalIncome = Math.Round(salary, 2)
            };
        }

        private async Task<int> GetTotalWorkingDateOfAMonth(DateTime date)
        {
            // Get the start and end date of the month for the given date
            DateTime startDate = new DateTime(date.Year, date.Month, 1);


            DateTime endDate = startDate.AddMonths(1).AddDays(-startDate.Day);
            int totalDays = (endDate - startDate).Days + 1; // add 1 to include the last day

            var holidays = await _holidayRepository.GetHolidaysByDateRangeAsync(new DateTime(date.Year, date.Month, 1),
            endDate);

            var listHoliday = new List<DateTime>();

            foreach (var holiday in holidays)
            {
                if (holiday.IsPaid == false)
                {

                    try
                    {
                        for (DateTime mDate = (DateTime)holiday.StartDate; mDate <= holiday.EndDate; mDate = mDate.AddDays(1))
                        {
                            if (date.Month == mDate.Month)
                            {
                                listHoliday.Add(mDate);
                            }
                        }
                    }
                    catch { }
                }

            }

            return Enumerable.Range(0, totalDays)
                .Select(d => startDate.AddDays(d))
                .Count(date => date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday && !listHoliday.Contains(date));
        }

    private async Task<int> GetTotalWorkingDaysInMonth(DateTime date, DateTime contractStartDate)
        {
            // Get the start and end date of the month for the given date
            DateTime startDate = new DateTime(date.Year, date.Month, 1);

            if(contractStartDate.Month == date.Month && contractStartDate.Year == date.Year)
            {
                startDate = contractStartDate;
            }

            DateTime endDate = startDate.AddMonths(1).AddDays(-startDate.Day);
            int totalDays = (endDate - startDate).Days + 1; // add 1 to include the last day

            var holidays = await _holidayRepository.GetHolidaysByDateRangeAsync(new DateTime(date.Year, date.Month, 1),
              endDate);

            var listHoliday = new List<DateTime>();

            foreach (var holiday in holidays)
            {
                if (holiday.IsPaid == false)
                {

                    try
                    {
                        for (DateTime mDate = (DateTime)holiday.StartDate; mDate <= holiday.EndDate; mDate = mDate.AddDays(1))
                        {
                            if (date.Month == mDate.Month)
                            {
                                listHoliday.Add(mDate);
                            }
                        }
                    }
                    catch { }
                }

            }

            return Enumerable.Range(0, totalDays)
                .Select(d => startDate.AddDays(d))
                .Count(date => date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday && !listHoliday.Contains(date));
        }


    }
}
