using CuisineCarousel.Models;

namespace CuisineCarousel;

public interface IFabricator
{
    Task<Uri> Fabricate(Recipe recipe);
}
