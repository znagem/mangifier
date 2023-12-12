using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace Mangifier.Api.MiddleWares;

public sealed class GlobalErrorHandlingMiddleWare(ILogger log) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (OperationCanceledException)
        {
            //ignore
        }
        catch (Exception e)
        {
            log.Error(e, "Internal server error occured: {Message}", e.Message);

            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

            ProblemDetails problem = new()
            {
                Status = (int) HttpStatusCode.InternalServerError,
                Title = "Internal server error occured",
                Type = "Server error",
                Detail = e.Message
            };

            var json = JsonSerializer.Serialize(problem);

            await context.Response.WriteAsync(json);
            context.Request.ContentType = "application/json";
        }
    }
}