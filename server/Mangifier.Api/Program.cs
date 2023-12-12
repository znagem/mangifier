using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using Mangifier.Api;
using Mangifier.Api.Services;

var builder = WebApplication.CreateBuilder(args);

const string corsOrigin = "_corsOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(corsOrigin,
        policy =>
        {
            policy.WithOrigins(
                    "http://localhost:5000")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.ConfigureKestrelInProduction();
builder.Services.AddHostedService<StartupService>();
builder.Services.AddLoggingServices();
builder.Services.AddDataAccessServices();
builder.Services.AddOtherServices();
builder.Services.AddAuthorization();
builder.Services.AddFastEndpoints();
builder.Services.SwaggerDocument();
builder.Services.AddJWTBearerAuth(builder.Configuration["JWTKey"]!,
    bearerEvents: o => o.OnMessageReceived = ctx => ctx.SetupTokenFilterAsync());

var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseFastEndpoints(c => { c.Endpoints.RoutePrefix = "api"; });
app.UseSwaggerGen();
app.UseMiddlewares();

app.Run();