using SalaryManagement.Domain.Entities;

namespace SalaryManagement.Application.Services.AdminServices
{
    public interface IAdminServices
    {
      //  Task<IEnumerable<Admin>> GetAllAdmins();
        Admin? GetAdminById(string id);
        //Admin CreateAdmin(Admin admin);
        Admin? UpdateAdmin(string id, Admin admin);
      //  Task DeleteAdmin(int id);
    }
}
