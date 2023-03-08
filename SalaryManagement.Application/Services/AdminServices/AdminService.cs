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

        public async Task<Admin>  GetAdminById(string id)
        {
           var admin = await _adminRepository.GetAdmin(id);

            if(admin == null)
            {
                return null;
            }

            return admin;
        }

        public async Task<bool>  UpdateAdmin(Admin admin)
        {
            return await _adminRepository.UpdateAdmin(admin);
        }
    }
}
