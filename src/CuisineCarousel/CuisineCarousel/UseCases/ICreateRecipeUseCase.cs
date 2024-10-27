using CuisineCarousel.Models;

namespace CuisineCarousel.UseCases;

public interface ICreateRecipeUseCase
{
    public Task<Recipe> CreateRecipeAsync(string originalDishId, string twistId);
}
