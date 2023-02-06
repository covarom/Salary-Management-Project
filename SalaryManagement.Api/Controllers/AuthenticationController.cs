using MapsterMapper;
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
        private readonly IMapper _mapper;

        public AuthenticationController(IAuthenticationServices authenticationServices, IMapper mapper) {
            _authenticationServices = authenticationServices;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            await Task.CompletedTask;

            var authResult = _authenticationServices.Register(request.FirstName, request.Lastname, request.Email, request.Password);
            AuthenticationResponse response = _mapper.Map<AuthenticationResponse>(authResult); //MapResponse(authResult);
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            await Task.CompletedTask;

            var authResult = _authenticationServices.Login(request.Email, request.Password);
            AuthenticationResponse response = _mapper.Map<AuthenticationResponse>(authResult);
            return Ok(response);
        }

       /* private static AuthenticationResponse MapResponse(AuthenticationResult authResult)
        {
            return new AuthenticationResponse(
                   authResult.User.Id,
                   authResult.User.FirstName,
                   authResult.User.LastName,
                   authResult.User.Email,
                   authResult.Token
                );
        }*/
    }
}
