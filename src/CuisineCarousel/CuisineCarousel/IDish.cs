using CuisineCarousel.Models;

namespace CuisineCarousel;

public interface IDish
{
    public Task<Dish> SaveAsync(Recipe recipe, Uri imageLocation);

    public Task<IEnumerable<Dish>> GetAllAsync();
}