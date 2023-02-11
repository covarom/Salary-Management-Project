using SalaryManagement.Domain.Entities;

namespace SalaryManagement.Application.Services.UserServices
{
    public interface IUserService
    {
        User GetUserById(int id);
        User GetUserByEmail(string email);
        IEnumerable<User> GetUsers();
        bool Add(User user);
    }
}
