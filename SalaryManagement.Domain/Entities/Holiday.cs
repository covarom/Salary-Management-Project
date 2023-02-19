using System;
using System.Collections.Generic;

namespace SalaryManagement.Domain.Entities;

public partial class Holiday
{
    public string HolidayId { get; set; } = null!;

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public bool? IsDeleted { get; set; }

    public string? HolidayName { get; set; }
}
