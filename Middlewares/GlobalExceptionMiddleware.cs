using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MyWebApiApp.Models.DTOs;
using System;
using System.Threading.Tasks;

namespace MyWebApiApp.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context); // Call the next middleware
            }
            catch (Exception ex)
            {
                // _logger.LogError(ex, "Unhandled exception occurred.");

                context.Response.StatusCode = ex switch
                {
                    ArgumentNullException => StatusCodes.Status400BadRequest,
                    UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                    _ => StatusCodes.Status500InternalServerError
                };

                Console.WriteLine(ex);
                context.Response.ContentType = "application/json";

                var response = new ApiResponse(ex.Message,context.Response.StatusCode);

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
