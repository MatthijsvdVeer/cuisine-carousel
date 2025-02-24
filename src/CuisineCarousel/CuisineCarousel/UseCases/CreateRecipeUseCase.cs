using CuisineCarousel.Models;

namespace CuisineCarousel.UseCases;

internal sealed class CreateRecipeUseCase(IOriginalDish originalDish, ITwist twist, IRecipe recipe) : ICreateRecipeUseCase
{
    public async Task<Recipe> CreateRecipeAsync(Guid originalDishId, string twistId)
    {
        var foundDish = await originalDish.GetById(originalDishId);
        var foundTwist = twist.GetById(twistId);
        return await recipe.CreateRecipeAsync(foundDish.Name, foundDish.Description, foundTwist.Description);
    }
}
