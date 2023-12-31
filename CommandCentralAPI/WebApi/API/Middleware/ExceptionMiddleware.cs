using System.Net;
using API.Middleware.Models;
using Application.Exceptions;

namespace API.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(httpContext, e);
        }
    }

    private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
        ErrorResponse errorResponse = new();
        var path = httpContext.Request.Path;

        switch (exception)
        {
            case BadRequestException badRequestException:
                statusCode = HttpStatusCode.BadRequest;
                errorResponse = new ErrorResponse
                {
                    // Type = "/errors/badrequesterror",
                    Type = nameof(BadRequestException),
                    Title = "Bad request exception",
                    Status = (int)statusCode,
                    Detail = badRequestException.Message,
                    Instance = path,
                    Errors = badRequestException.ValidationErrors
                };
                break;
            case NotFoundException notFoundException:
                statusCode = HttpStatusCode.NotFound;
                errorResponse = new ErrorResponse
                {
                    Type = nameof(NotFoundException),
                    Title = "Not found exception",
                    Status = (int)statusCode,
                    Detail = notFoundException.Message,
                    Instance = path
                };
                break;
            case AuthorizationException authorizationException:
                statusCode = HttpStatusCode.Forbidden;
                errorResponse = new ErrorResponse
                {
                    Type = nameof(AuthorizationException),
                    Title = "Access Denied",
                    Status = (int)statusCode,
                    Detail = authorizationException.Message,
                    Instance = path,
                };
                break;
            default:
                errorResponse = new ErrorResponse
                {
                    Title = exception.Message,
                    Status = (int)statusCode,
                    Detail = exception.InnerException?.Message,
                    Instance = path,
                    Type = nameof(exception),
                };
                break;
                
        }
        
        httpContext.Response.StatusCode = (int)statusCode;
        await httpContext.Response.WriteAsJsonAsync(errorResponse);
    }
}