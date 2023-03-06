using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.IdentityModel.Tokens;

namespace SalaryManagement.Api.Common.Errors;
public class SalaryManagementProblemDetailsFactory : ProblemDetailsFactory
{

    private readonly ApiBehaviorOptions _options;

    public SalaryManagementProblemDetailsFactory(IOptions<ApiBehaviorOptions> options)
    {
        _options = options?.Value ??  throw new ArgumentNullException(nameof(options));
    }

    public override ProblemDetails CreateProblemDetails(HttpContext httpContext, int? statusCode = null, string? title = null, string? type = null, string? detail = null, string? instance = null)
    {
        var problemDetail = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Type = type,
            Detail = detail,
            Instance = instance,
        };

        if (problemDetail.Title.IsNullOrEmpty())
        {
            problemDetail.Title = GetTitleForStatusCode(statusCode);
        }

        return problemDetail;
    }

    public override ValidationProblemDetails CreateValidationProblemDetails(HttpContext httpContext, ModelStateDictionary modelStateDictionary, int? statusCode = null, string? title = null, string? type = null, string? detail = null, string? instance = null)
    {
        var problemDetails = new ValidationProblemDetails(modelStateDictionary)
        {
            Status = statusCode,
            Title = title,
            Type = type,
            Detail = detail,
            Instance = instance
        };

        if (problemDetails.Title.IsNullOrEmpty())
        {
            problemDetails.Title = GetTitleForStatusCode(statusCode);
        }

        return problemDetails;
    }

    private string GetTitleForStatusCode(int? statusCode)
    {
        switch (statusCode)
        {
            case 400:
                return "Bad Request";
            case 401:
                return "Unauthorized";
            case 403:
                return "Forbidden";
            case 404:
                return "Not Found";
            case 500:
                return "Internal Server Error";
            default:
                return "An error occured";
        }
    }
}
