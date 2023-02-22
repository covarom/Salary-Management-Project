using Mapster;
using SalaryManagement.Contracts.Employees;
using SalaryManagement.Domain.Entities;

namespace SalaryManagement.Api.Common.Mapping
{
    public class EmployeeMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Employee, EmployeeResponse>()
                .Map(dest => dest.EmployeeId, src => src.EmployeeId)
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Image, src => src.Image)
                .Map(dest => dest.DateOfBirth, src => src.DateOfBirth)
                .Map(dest => dest.Address, src => src.Address)
                .Map(dest => dest.IdentifyNumber, src => src.IdentifyNumber)
                .Map(dest => dest.IsActive, src => src.IsActive)
                .Map(dest => dest.PhoneNumber, src => src.PhoneNumber);
        }
    }
}
