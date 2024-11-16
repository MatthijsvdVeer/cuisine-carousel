namespace CuisineCarousel.Init;

internal interface IInitHandler
{
    Task RunAsync(CancellationToken cancellationToken = default);
}
