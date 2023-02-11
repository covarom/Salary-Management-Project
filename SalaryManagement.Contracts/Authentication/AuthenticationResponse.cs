using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryManagement.Contracts.Authentication
{
    public record AuthenticationResponse(
         string Id,
         string Name,
         string PhoneNumber,
         string UserName,
         string Token);
}
