namespace SalaryManagement.Domain.Entities;

public partial class Admin
{
    public string AdminId { get; set; } = null!;

    public string? Name { get; set; }

    public string? PhoneNumber { get; set; }

    public string? IsActive { get; set; } = "1";

    public string? Username { get; set; }

    public string? Password { get; set; }
}
