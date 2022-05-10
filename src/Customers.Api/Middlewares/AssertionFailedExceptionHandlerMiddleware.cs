using Customers.Model.Exceptions;

namespace Customers.Api.Middlewares
{
    public sealed class AssertionFailedExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public AssertionFailedExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (AssertionConcernFailedException exception)
            {
                await WriteToResponseBodyAsync(context, 400, exception);
            }
            catch (EntityAlreadyExistsException exception)
            {
                await WriteToResponseBodyAsync(context, 409, exception);
            }
            catch (EntityNotFoundException exception)
            {
                await WriteToResponseBodyAsync(context, 404, exception);
            }
        }

        private static async Task WriteToResponseBodyAsync(HttpContext context, int responseCode, Exception exception)
        {
            context.Response.StatusCode = responseCode;
            context.Response.ContentType = "text/plain";
            await context.Response.WriteAsync(exception.Message);
        }
    }

    public static class AssertionFailedExceptionHandlerExtensions
    {
        public static IApplicationBuilder UseAssertionFailedExceptionHandler(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AssertionFailedExceptionHandlerMiddleware>();
        }
    }
}