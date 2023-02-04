using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryManagement.Contracts.Authentication
{
    public record AuthenticationResponse(
         Guid Id,
         string FirstName,
         string Lastname,
         string Email,
         string token);
}
