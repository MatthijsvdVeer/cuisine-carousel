using CuisineCarousel.Models;

namespace CuisineCarousel;

internal sealed class OfflineFabricator : IFabricator
{
    public Task<Uri> Fabricate(Recipe recipe)
    {
        return Task.FromResult(new Uri("GordonRamsAI.webp", UriKind.Relative));
    }
}