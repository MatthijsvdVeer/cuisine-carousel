using CuisineCarousel.Models;

namespace CuisineCarousel.UseCases;

internal sealed class CreateRecipeUseCase(IOriginalDish originalDish, ITwist twist, IRecipe recipe) : ICreateRecipeUseCase
{
    public Task<Recipe> CreateRecipeAsync(string originalDishId, string twistId)
    {
        var foundDish = originalDish.GetById(originalDishId);
        var foundTwist = twist.GetById(twistId);
        return recipe.CreateRecipeAsync(foundDish.Name, foundDish.Description, foundTwist.Description);
    }
}
