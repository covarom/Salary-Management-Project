using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalaryManagement.Contracts.Response;
using SalaryManagement.Domain.Entities;
using System.Net;

namespace SalaryManagement.Api.Controllers
{
    [Route("api/v1/test")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Test()
        {
            var statusCode = HttpStatusCode.OK;
            var response = new Response<object>
            {
                Message = "For testing purpose",
            };

            try
            {
                List<User> users = new List<User>();

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
                });

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
