using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace NzWalksAPI.Core.Middlewares
{
    public class ExceptionHandlerMiddleware
    (ILogger<ExceptionHandlerMiddleware> logger, RequestDelegate next)
    {
        private readonly ILogger<ExceptionHandlerMiddleware> logger = logger;
        private readonly RequestDelegate next = next;

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid();
                logger.LogError(ex, $"{errorId}: {ex.Message}");

                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";

                var error = new
                {
                    Id = errorId,
                    ErrorMessage = "Something went wrong."
                };

                await httpContext.Response.WriteAsJsonAsync(error);
            }
        }
    }
}