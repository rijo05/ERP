using FluentValidation;

namespace ERP.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (ValidationException ex)
        {
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

            httpContext.Response.ContentType = "application/json";

            var erros = ex.Errors.Select(e => new
            {
                field = e.PropertyName,
                message = e.ErrorMessage
            });

            await httpContext.Response.WriteAsJsonAsync(new
            {
                error = "Validation Failed",
                errors = erros
            });

        }
        catch ( Exception ex)
        {
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            httpContext.Response.ContentType = "application/json";

            await httpContext.Response.WriteAsJsonAsync(new
            {
                error = "Internal Server Error",
                errors = ex.Message
            });
        }
    }

}
