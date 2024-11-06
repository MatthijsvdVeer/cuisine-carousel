using CuisineCarousel.Models;

namespace CuisineCarousel.UseCases;

internal sealed class CreateCollaborativeRecipeUseCase(IOriginalDish originalDish, ITwist twist, IRecipe recipe) : ICreateCollaborativeRecipeUseCase
{
    public IAsyncEnumerable<CollaborationStep> CreateRecipeAsync(string originalDishId, string twistId)
    {
        var foundDish = originalDish.GetById(originalDishId);
        var foundTwist = twist.GetById(twistId);
        return recipe.CollaborateOnRecipeAsync(foundDish.Name, foundDish.Description, foundTwist.Name);
    }
}
