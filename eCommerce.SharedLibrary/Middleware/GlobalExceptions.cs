using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Serilog;
using System.Net;

namespace eCommerce.SharedLibrary.Middleware
{
    public class GlobalExceptions
    {
        private readonly RequestDelegate _next;

        public GlobalExceptions(RequestDelegate next) => _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
           
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Unhandled exception");
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";
                var payload = JsonSerializer.Serialize(new { error = "An unexpected error occurred." });
                await context.Response.WriteAsync(payload);
            }
        }
    }
}