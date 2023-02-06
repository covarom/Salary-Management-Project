using System.Net;

namespace SalaryManagement.Application.Common.Errors
{
    public class DuplicateEmailException : Exception, IServiceException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.Conflict;

        public string Message => "Email is already exists!";
    }
}
