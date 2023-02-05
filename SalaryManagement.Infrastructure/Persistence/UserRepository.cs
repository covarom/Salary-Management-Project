using SalaryManagement.Application.Common.Interfaces.Persistence;
using SalaryManagement.Domain.Entities;

namespace SalaryManagement.Infrastructure.Persistence
{
    public class UserRepository : IUserRepository
    {
        private static readonly List<User> _users = new List<User>();
        public bool Add(User user)
        {
            try
            {
                _users.Add(user);
            }
            catch { 
                return false;
            }

            return true;
        }

        public User? GetUserByEmail(string email)
        {
           return _users.SingleOrDefault(x => x.Email == email);
        }
    }
}
