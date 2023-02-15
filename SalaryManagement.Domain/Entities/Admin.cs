using System;
using System.Collections.Generic;

namespace SalaryManagement.Domain.Entities;

public partial class Admin
{
    public string AdminId { get; set; } = null!;

    public string? Name { get; set; }

    public string? Image { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public bool? IsActive { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;
}
