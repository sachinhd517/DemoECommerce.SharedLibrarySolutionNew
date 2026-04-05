using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Serilog;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using eCommerce.SharedLibrary.Logs;

namespace eCommerce.SharedLibrary.Middleware
{
    public class GlobalExceptions
    {
        private readonly RequestDelegate _next;

        public GlobalExceptions(RequestDelegate next) => _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            // Declare variables

            string message = "Sorry, internal server error occurred. Kingly try again";
            int statusCode = (int)HttpStatusCode.InternalServerError;
            string title = "Error";
           
            try
            {
                await _next(context);

                // Check if Response is Too many Request // 429 status code.

                if(context.Response.StatusCode == StatusCodes.Status429TooManyRequests)
                {
                    title = "Warning";
                    message = "Too many requestmade";
                    statusCode = (int)StatusCodes.Status429TooManyRequests;
                    await ModifyHeader(context, title, message, statusCode);
                }

                // If Response is UnAuthorized // 401 status code.

                if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
                {
                    title = "Alert";
                    message = "You are not authorized to access this resource";
                    statusCode = (int)StatusCodes.Status401Unauthorized;
                    await ModifyHeader(context, title, message, statusCode);
                }

                // If Response is Not Found // 403 status code.

                if (context.Response.StatusCode == StatusCodes.Status403Forbidden)
                {
                    title = "Warning";
                    message = "Resource not found";
                    statusCode = (int)StatusCodes.Status403Forbidden;
                    await ModifyHeader(context, title, message, statusCode);
                }
            }
            catch (Exception ex)
            {
                // Log Original Exception /File, Console, Debugger
                LogException.LogExceptions(ex);

                // check if Exception is Timeout Exception //408 request timeout status code.
                if (ex is TaskCanceledException || ex is TaskCanceledException)
                {
                    title = "Out of Time";
                    message = "Request Timeout...... try againS";
                    statusCode = (int)StatusCodes.Status408RequestTimeout;
                   
                }
                // If Exception is caught.
                // If none of the exceptions the do the default
                await ModifyHeader(context, title, message, statusCode);



                //Log.Error(ex, "Unhandled exception");
                //context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                //context.Response.ContentType = "application/json";
                //var payload = JsonSerializer.Serialize(new { error = "An unexpected error occurred." });
                //await context.Response.WriteAsync(payload);
            }
        }

        private static async Task ModifyHeader(HttpContext context, string title, string message, int statusCode)
        {
            // displauy error message
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";
            var payload = JsonSerializer.Serialize(new { error = message });
            await context.Response.WriteAsync(JsonSerializer.Serialize(new ProblemDetails() 
            { 
                Status = statusCode,
                Title = title, 
                Detail = message 
            }), CancellationToken.None);
            return;
        }
    }
}