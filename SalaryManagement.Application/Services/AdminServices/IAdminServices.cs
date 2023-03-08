using SalaryManagement.Domain.Entities;
using System.Security.Cryptography;
using System.Text;

namespace SalaryManagement.Application.Services.AdminServices
{
    public interface IAdminServices
    {
      //  Task<IEnumerable<Admin>> GetAllAdmins();
        Task<Admin> GetAdminById(string id);
        //Admin CreateAdmin(Admin admin);
        Task<bool> UpdateAdmin(Admin admin);
      //  Task DeleteAdmin(int id);
        string HashPassword(string password){
          SHA256 hash = SHA256.Create();
          var passwordBytes = Encoding.Default.GetBytes(password);
          var hasedpassword = hash.ComputeHash(passwordBytes);
          return Convert.ToHexString(hasedpassword);
        }
    }
}
