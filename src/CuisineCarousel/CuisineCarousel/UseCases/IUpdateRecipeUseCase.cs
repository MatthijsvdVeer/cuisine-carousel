using CuisineCarousel.Models;

namespace CuisineCarousel.UseCases;

public interface IUpdateRecipeUseCase
{
    public Task<Recipe> UpdateRecipeAsync(Recipe recipe, string instructions);
}
