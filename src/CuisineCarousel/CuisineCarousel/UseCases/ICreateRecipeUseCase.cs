using CuisineCarousel.Models;

namespace CuisineCarousel.UseCases;

public interface ICreateRecipeUseCase
{
    public Task<Recipe> CreateRecipeAsync(Guid originalDishId, string twistId);
}
