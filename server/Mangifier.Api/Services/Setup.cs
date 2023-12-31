using System.Net;
using System.Runtime.InteropServices;
using Mangifier.Api.MiddleWares;
using Mangifier.Api.Services.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Mangifier.Api.Services;

public static class Setup
{
    public static void ConfigureKestrelInProduction(this WebApplicationBuilder builder)
    {
        if (builder.Environment.IsDevelopment())
            builder.WebHost.ConfigureKestrel(options =>
            {
                options.Limits.MaxRequestBodySize = 1073741824;
            });
        else
            builder.WebHost.ConfigureKestrel(options =>
            {
                options.Limits.MaxRequestBodySize = 1073741824;
                var ip = IpAddressHelper.GetIpAddress();
                options.Listen(IPAddress.Parse(ip), 5181);
                options.Listen(IPAddress.Parse("127.0.0.1"), 5180);
            });
    }

    public static void AddLoggingServices(this IServiceCollection services)
    {
        services.AddSingleton<ILogger>(_ =>
        {
            var basePath = string.Empty;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                basePath = Environment.ExpandEnvironmentVariables(@"%USERPROFILE%\.Mangifier");

            var logPath = Path.Combine(basePath, "logs");
            return new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(Path.Combine(logPath, "server-logs.txt"), rollingInterval: RollingInterval.Day)
                .CreateLogger();
        });

        services.AddTransient<GlobalErrorHandlingMiddleWare>();
    }

    public static void AddOtherServices(this IServiceCollection services)
    {
        services.AddSingleton<AuthService>();
    }

    public static void UseMiddlewares(this WebApplication app)
    {
        app.UseMiddleware<GlobalErrorHandlingMiddleWare>();
    }

    public static async Task SetupTokenFilterAsync(this MessageReceivedContext ctx)
    {
        ctx.HttpContext.Request.Headers.TryGetValue("Authorization", out var authHeader);
        if (authHeader.Count == 0) return;

        if (!authHeader.ToString().StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
        {
            ctx.Fail("Token is invalid");
            return;
        }

        ctx.Token = authHeader.ToString()["Bearer ".Length..].Trim();

        var authService = ctx.HttpContext.RequestServices.GetRequiredService<AuthService>();
        var cts = ctx.HttpContext.RequestAborted;
        var blacklisted = await authService.IsBlacklistedAsync(ctx.Token, cts);
        if (blacklisted)
        {
            ctx.Response.Headers.Append("Auth-Error", "Your access has been revoked");
            ctx.Fail("Token is invalid");
        }
    }
}