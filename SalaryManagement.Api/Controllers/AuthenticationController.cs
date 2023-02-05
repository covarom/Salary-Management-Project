using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalaryManagement.Application.Services.Authentication;
using SalaryManagement.Contracts.Authentication;

namespace SalaryManagement.Api.Controllers
{
    [Route("api/v1/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationServices _authenticationServices;

        public AuthenticationController(IAuthenticationServices authenticationServices) {
            _authenticationServices = authenticationServices;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterRequest request)
        {
            var authResult = _authenticationServices.Register(request.FirstName, request.Lastname, request.Email, request.Password);
            var response = new AuthenticationResponse(
                   authResult.User.Id,
                   authResult.User.FirstName,
                   authResult.User.LastName,
                   authResult.User.Email,
                   authResult.Token
                );
            return Ok(response);
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            var authResult = _authenticationServices.Login(request.Email, request.Password);
            var response = new AuthenticationResponse(
                   authResult.User.Id,
                   authResult.User.FirstName,
                   authResult.User.LastName,
                   authResult.User.Email,
                   authResult.Token
                );
            return Ok(response);
        }
    }
}
