using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryManagement.Contracts.Authentication
{
    public record RegisterRequest(
    string Username,
    string Password,
    string Email,
    string Name,
    string PhoneNumber);
       
}
