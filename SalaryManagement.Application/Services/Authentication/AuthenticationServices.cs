using SalaryManagement.Application.Common.Interfaces.Authentication;
using SalaryManagement.Application.Common.Interfaces.Persistence;
using SalaryManagement.Domain.Entities;

namespace SalaryManagement.Application.Services.Authentication
{
    public class AuthenticationServices : IAuthenticationServices
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public AuthenticationServices(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public AuthenticationResult Login(string email, string password)
        {
            // Validate if the user exists
            if (_userRepository.GetUserByEmail(email) is not User user)
            {
                throw new Exception("User is not exist!");
            }

            // Validate if the password is correct
            if (password != user.Password) {
                throw new Exception("Invalid password");
            }

            // Create token
            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(
                user,
                token);
        }

        public AuthenticationResult Register(string firstName, string lastName, string email, string password)
        {
            //Check if the user is already exists
            if (_userRepository.GetUserByEmail(email) is not null)
            {
                throw new Exception("User is already exist!");
            }

            //Create new user and store to DB
            Guid userId = Guid.NewGuid();
            var user = new User
            {
                Id= userId,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password
            };
            bool success =  _userRepository.Add(user);

            if (!success)
            {
                throw new Exception("An error has occur, please contact your admin!");
            }

            //Create JWT token
            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(
             user,
             token);
        }
    }
}
