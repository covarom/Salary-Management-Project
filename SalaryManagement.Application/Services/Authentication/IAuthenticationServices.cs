namespace SalaryManagement.Application.Services.Authentication
{
    public interface IAuthenticationServices
    {
        AuthenticationResult Login(string email, string password);

        AuthenticationResult Register(string name, string phoneNumber, string email, string username, string password);
    }
}
    