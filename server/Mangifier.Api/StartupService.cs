using Mangifier.Api.DataAccess;

namespace Mangifier.Api;

internal sealed class StartupService(
    ILogger log,
    DbInitializer dbInitializer,
    IHostApplicationLifetime appLifetime)
    : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        appLifetime.ApplicationStarted.Register(OnStarted);
        appLifetime.ApplicationStopping.Register(OnStopping);
        appLifetime.ApplicationStopped.Register(OnStopped);

        return dbInitializer.InitAsync();
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    private void OnStarted()
    {
        log.Information("Service started");
    }

    private void OnStopping()
    {
        log.Information("Service stopping");
    }

    private void OnStopped()
    {
        log.Information("Service stopped");
    }
}