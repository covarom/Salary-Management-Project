using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalaryManagement.Application.Common.Errors;

namespace SalaryManagement.Api.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        [Route("/error")]
        public IActionResult Error(int code)
        {
            Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>().Error;

            var (statusCode, message) = exception switch
            {
                IServiceException serviceException => ((int)serviceException.StatusCode, serviceException.Message),
                _ => (StatusCodes.Status500InternalServerError, "Something unexpected has occured.")    ,
            };
            return Problem(statusCode: statusCode, title: message);
        }
    }
}
