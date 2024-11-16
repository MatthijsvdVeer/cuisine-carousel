namespace CuisineCarousel.Init;

using Microsoft.Extensions.Hosting;

internal class MainHostedService(IEnumerable<IInitHandler> initHandlers, IHostApplicationLifetime lifetime)
    : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var tasks = initHandlers.Select(x => x.RunAsync(cancellationToken));
        await Task.WhenAll(tasks);

        lifetime.StopApplication();
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}