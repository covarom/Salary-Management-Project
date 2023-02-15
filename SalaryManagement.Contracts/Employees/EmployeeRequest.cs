namespace SalaryManagement.Contracts.Employee
{
    public record EmployeeRequest(
        string Employee_name,
        string image,
        DateTime day_of_birth,
        string address,
        int identify_number,
        string PhoneNumber
        );
    public record EmployeeUpdate(
        string Id,
        string? Employee_name,
        string? Image,
        DateTime? Day_of_birth,
        string? Address,
        bool? IsActive,
        int? Identify_number,
        string? PhoneNumber
        );    
    public record EmployeeDelete(
        string id
        );  
}
