using eCommerce.SharedLibrary.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.SharedLibrary.DependancyInjection
{
    public static class SharedServiceContainer
    {
        public static IServiceCollection AddSharedServices<TContext>
            (this IServiceCollection service, IConfiguration config, string fileName) where TContext : DbContext
        {
            service.AddDbContext<TContext>(options =>

                options.UseSqlServer(config.GetConnectionString("eCommerceConnection"), sqlserverOption => sqlserverOption.EnableRetryOnFailure()));

            //  configuration serilog logging
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Debug()
                .WriteTo.Console()
                .WriteTo.File(path: $"{fileName}-.text",
                restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information,
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {message:lj} {newLine} {Exception}",
                rollingInterval: RollingInterval.Day)
                .CreateLogger();


            // Add JWT Authentication Schema
            JWTAuthenticationSchema.AddJWTAuthenticationSchema(service, config);
            return service;
        }

        public static IApplicationBuilder UseSharedPolicies(this IApplicationBuilder app)
        {
            // Use golobal Exception
            app.UseMiddleware<GlobalExceptions>();

            // Register middleware to block all outsiders API calls
           // app.UseMiddleware<ListenToOnlyApiGateway>();

            return app;
        }
    }
    
}
