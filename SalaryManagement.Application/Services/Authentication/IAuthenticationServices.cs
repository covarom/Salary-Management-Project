namespace SalaryManagement.Application.Services.Authentication
{
    public interface IAuthenticationServices
    {
        AuthenticationResult Login (string email, string password);

        AuthenticationResult Register (string name, string phoneNumber, string username, string password);
    }
}
    