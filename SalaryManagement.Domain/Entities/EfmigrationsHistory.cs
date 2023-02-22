using System;
using System.Collections.Generic;

namespace SalaryManagement.Domain.Entities;

public partial class EfmigrationsHistory
{
    public string MigrationId { get; set; } = null!;

    public string ProductVersion { get; set; } = null!;
}
