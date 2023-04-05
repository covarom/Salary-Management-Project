using SalaryManagement.Application.Common.Errors;
using SalaryManagement.Application.Common.Interfaces.Authentication;
using SalaryManagement.Application.Common.Interfaces.Persistence;
using SalaryManagement.Application.Services.AdminServices;
using SalaryManagement.Domain.Entities;
using System.Net;

namespace SalaryManagement.Application.Services.Authentication
{
    public class AuthenticationServices : IAuthenticationServices
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IAdminRepository _adminRepository;
        private readonly IAdminServices _adminServices;

        public AuthenticationServices(IJwtTokenGenerator jwtTokenGenerator, IAdminRepository adminRepository,
          IAdminServices adminServices)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _adminRepository = adminRepository;
            _adminServices= adminServices;
        }

        public AuthenticationResult Login(string username, string password)
        {
            var passwordHash = _adminServices.HashPassword(password);
            // Validate if the user exists
            if (_adminRepository.GetAdminByUsernameAndPassword(username,passwordHash) is not Admin admin)
            {               
                throw new Exception("An error has occur, please contact your admin!");            
            }

            // Create token
            var token = _jwtTokenGenerator.GenerateToken(admin);

            return new AuthenticationResult(
                     admin,
                     token);
        }

        public AuthenticationResult Register(string name, string phoneNumber, string email, string username, string password)
        {
            //Check if the user is already exists
            if (_adminRepository.GetAdminByUsername(username) is not null)
            {
                throw new Exception("Username has already exist!");
            }

            //Create new user and store to DB
            string adminId = Guid.NewGuid().ToString();
            var admin = new Admin
            {
                AdminId = adminId,
                Name = name,
                PhoneNumber = phoneNumber,
                Email = email,
                Username = username,
                Password = password,
                IsActive = true,
                IsFirstLogin = true
            };

            bool success = _adminRepository.AddAdmin(admin);

            if (!success)
            {
                throw new Exception("An error has occurred, please contact your admin!");
            }

            //Create JWT token
            var token = _jwtTokenGenerator.GenerateToken(admin);

            return new AuthenticationResult(
                     admin,
                     token
                     );
        }
    }
}
