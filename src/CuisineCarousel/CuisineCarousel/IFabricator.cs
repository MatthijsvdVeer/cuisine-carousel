using CuisineCarousel.Models;

namespace CuisineCarousel;

public interface IFabricator
{
    Task Fabricate(Recipe recipe);
}
