using eCommerce.SharedLibrary.Logs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace eCommerce.SharedLibrary.Middleware
{
    public class GobalException(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            // Declare variable
            string mesaage = "Sorry, Internal Server Error occurred. Kindly try again";
            int statusCode = (int)HttpStatusCode.InternalServerError;
            string title = "Internal Server Error";
            try
            {
                await next(context);

                // check if Response is Too Many Request // 429 status code
                if (context.Response.StatusCode == (int)HttpStatusCode.TooManyRequests)
                {
                    title = "warning";
                    mesaage = "Sorry, Too Many Request. Kindly try again later";
                    statusCode = (int)StatusCodes.Status429TooManyRequests; ;
                    await ModifyHeader(context, title, mesaage, statusCode);
                }
                // if Response is UnAutherized // 401 status code
                if(context.Response.StatusCode  == statusCode)
                {
                    title = "Alert";
                    mesaage = "Sorry,You are not Autherized to access. Kindly login to access this resource";
                    statusCode = (int)HttpStatusCode.Unauthorized;
                    await ModifyHeader(context, title, mesaage, statusCode);
                }
                // If Response Forbidden // 403 status code
                if(context.Response.StatusCode == (int)HttpStatusCode.Forbidden)
                {
                    title = "Out of Access";
                    mesaage = "Sorry, You are not allowed to access this resource";
                    statusCode = (int)HttpStatusCode.Forbidden;
                    await ModifyHeader(context, title, mesaage, statusCode);
                }
            }
            catch (Exception ex)
            {

                // Log Original Exception / Console / Debugger / File 
                LogException.LogExceptions(ex);

                // check if Exception is Timeout
                if (ex is TaskCanceledException || ex is TimeoutException)
                {
                    title = "Out of Time & Timeout";
                    mesaage = "Sorry, Request Timeout. Kindly try again later";
                    statusCode = (int)HttpStatusCode.RequestTimeout;
                    await ModifyHeader(context, title, mesaage, statusCode);
                }
                // check if Exception is Caught.
                // if not of the exception then do the default
                await ModifyHeader(context, title, mesaage, statusCode);
            }
        }

        private static async Task ModifyHeader(HttpContext context, string title, string mesaage, int statusCode)
        {
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(new ProblemDetails()
            {
                Title = title,
                Detail = mesaage,
                Status = statusCode
            }), CancellationToken.None);
            return;
        }
    }
}
