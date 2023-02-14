using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalaryManagement.Api.Common.Helper;
using SalaryManagement.Application.Services.AdminServices;
using SalaryManagement.Application.Services.Authentication;
using SalaryManagement.Contracts;
using SalaryManagement.Contracts.Admin;
using System.IdentityModel.Tokens.Jwt;
using System.Net;

namespace SalaryManagement.Api.Controllers
{
    [Route("api/v1/auth")]
    [ApiController]
    [Authorize]
    public class AdminController : ControllerBase
    {
        private readonly IAdminServices _adminServices;
        private readonly IMapper _mapper;

        public AdminController(IAdminServices adminServices, IMapper mapper)
        {
            _adminServices = adminServices;
            _mapper = mapper;
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {

            var statusCode = HttpStatusCode.OK;
            var message = "";

            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            // Use the token for further processing.
            var admin = _adminServices.GetAdminById(JwtTokenHelper.GetClaimValue(token, JwtRegisteredClaimNames.Sub));

            if (admin == null)
            {
                statusCode = HttpStatusCode.NotFound;
                message = "not found the user";
                return NotFound(new Response<object>
                {
                    StatusCode = (int)statusCode,
                    Message = message,
                    Data = null
                });
            }

            AdminResponse response = _mapper.Map<AdminResponse>(admin);

            return Ok(new Response<object>
            {
                StatusCode = (int)statusCode,
                Message = message,
                Data = response
            });
        }
    }

}
