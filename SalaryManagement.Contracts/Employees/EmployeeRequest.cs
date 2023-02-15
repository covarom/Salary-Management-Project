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
        string id,
        string Employee_name,
        string image,
        DateTime day_of_birth,
        string address,
        bool IsActive,
        int identify_number,
        string PhoneNumber
        );    
    public record EmployeeDelete(
        string id
        );  
}
