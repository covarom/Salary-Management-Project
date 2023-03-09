using SalaryManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryManagement.Application.Common.Interfaces.Persistence
{
    public interface IAdminRepository
    {
         Task<Admin> GetAdmin(string id);

        IEnumerable<Admin> GetAll();

        Admin? GetAdminByUsername(string username);

        Admin? GetAdminByUsernameAndPassword(string username, string password);

        bool AddAdmin(Admin admin);

        Task<bool> UpdateAdmin(Admin admin);
    }
}
