using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalaryManagement.Application.Services.UserServices;
using SalaryManagement.Contracts.Response;
using System.Net;

namespace SalaryManagement.Api.Controllers
{
    [Route("api/v1")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class TestController : ControllerBase
    {
        private readonly IUserService _userService;

        public TestController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("get-users")]
        public IActionResult GetUsers()
        {
            var statusCode = HttpStatusCode.OK;
            var response = new Response<object>
            {
                Message = "For testing purpose",
            };

            try
            {
               /* List<User> users = new List<User>();

                users.Add(new User
                {
                    FirstName = "Nguyen",
                    LastName = "Giang",
                    Email = "GiangNTSE150747@fpt.edu.vn"
                });

                users.Add(new User
                {
                    FirstName = "Nguyen",
                    LastName = "Doan",
                    Email = "DoanNH@fpt.edu.vn"
                });*/

                var users = _userService.GetUsers();

                response.Data = users;
            }
            catch
            {
                statusCode = HttpStatusCode.InternalServerError;
            }


            response.StatusCode = (int)statusCode;

            return Ok(response);

            //return Ok("adssa");
        } 
    }
}
