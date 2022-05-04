using Customers.Model.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.Text;

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
                context.Response.StatusCode = 400;
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync(exception.Message);
            }
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