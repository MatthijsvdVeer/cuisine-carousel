using CuisineCarousel.Models;

namespace CuisineCarousel.UseCases;

public class UpdateRecipeUseCase(IRecipe recipeService) : IUpdateRecipeUseCase
{
    public Task<Recipe> UpdateRecipeAsync(Recipe recipe, string instructions)
    {
        return recipeService.UpdateRecipeAsync(recipe, instructions);
    }
}
