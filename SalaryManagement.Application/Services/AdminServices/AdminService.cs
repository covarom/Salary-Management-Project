using SalaryManagement.Application.Common.Interfaces.Persistence;
using SalaryManagement.Domain.Entities;

namespace SalaryManagement.Application.Services.AdminServices
{
    public class AdminService : IAdminServices
    {
        private readonly IAdminRepository _adminRepository;

        public AdminService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public Admin? GetAdminById(string id)
        {
           var admin = _adminRepository.GetAdmin(id);

            if(admin == null)
            {
                return null;
            }

            return admin;
        }

        public Admin? UpdateAdmin(string id, Admin admin)
        {
            var existingAdmin = _adminRepository.GetAdmin(id);
            if (existingAdmin == null)
            {
                return null;
            }

            existingAdmin.PhoneNumber = admin.PhoneNumber;
            existingAdmin.Name = admin.Name;
            existingAdmin.IsActive = admin.IsActive;

            return _adminRepository.UpdateAdmin(existingAdmin);
        }
    }
}
