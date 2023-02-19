namespace SalaryManagement.Application.Services.SalaryTypeService
{
    public record SalaryTypeRequest(
        string SalaryTypeName,
        bool IsDelete
        );
    public record SalaryTypeUpdate(
        string Id,
        string SalaryTypeName,
        bool IsDelete
        );
    public record SalaryTypeDelete(
        string Id
        );
}
