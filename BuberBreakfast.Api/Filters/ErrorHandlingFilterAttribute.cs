using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BuberBreakfast.Api.Filter;

public class ErrorHandlingFilterAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        var exception = context.Exception;
        
        var problemDetails = new ProblemDetails{
            Title = "An error occured while processing your request",
            Status = (int)HttpStatusCode.InternalServerError,   
        };

        context.Result = new ObjectResult(problemDetails);

        context.ExceptionHandled = true;
    }
}