﻿using SalaryManagement.Contracts.Salary;
using SalaryManagement.Domain.Entities;

namespace SalaryManagement.Application.Services.SalaryServices
{
    public interface ISalaryService
    {
        Task<SalaryResponse> CalculateSalaryAsync(Employee employee, int? otTime, int? leaveTime, DateTime date);


        Task<SalaryResponse> CalculateSalaryForPartnerAsync(Employee employee, int? otTime, int? leaveTime, DateTime date);

    }
}
