using SalaryManagement.Application.Common.Interfaces.Persistence;
using SalaryManagement.Domain.Entities;

namespace SalaryManagement.Infrastructure.Persistence.Repositories
{
    public class AdminRepository : IAdminRepository
    {

        private readonly SalaryManagementContext _context;

        public AdminRepository(SalaryManagementContext context)
        {
            _context = context;
        }

        public bool AddAdmin(Admin admin)
        {
            try
            {
                _context.Admins.Add(admin);
                _context.SaveChanges();
            }
            catch(Exception e)
            {
                return false;
            }
            return true;
        }

        public Admin? GetAdmin(string id)
        {
            return _context.Admins.SingleOrDefault(x => x.AdminId.Equals(id));
        }

        public Admin? GetAdminByUsername(string username)
        {
            return _context.Admins.SingleOrDefault(x => x.Username.Equals(username));
        }

        public Admin? GetAdminByUsernameAndPassword(string username, string password)
        {
            return _context.Admins.FirstOrDefault(x => x.Username.Equals(username) && x.Password.Equals(password));
        }

        public IEnumerable<Admin> GetAll()
        {
            return _context.Admins.ToList();
        }

        public Admin? UpdateAdmin(Admin admin)
        {

            _context.Admins.Update(admin);
            _context.SaveChanges();

            return admin;
        }
    }
}
