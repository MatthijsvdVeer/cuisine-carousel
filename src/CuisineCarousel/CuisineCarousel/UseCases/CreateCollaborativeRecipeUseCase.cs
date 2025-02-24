using CuisineCarousel.Models;

namespace CuisineCarousel.UseCases;

internal sealed class CreateCollaborativeRecipeUseCase(IOriginalDish originalDish, ITwist twist, IRecipe recipe) : ICreateCollaborativeRecipeUseCase
{
    public async IAsyncEnumerable<CollaborationStep> CreateRecipeAsync(Guid originalDishId, string twistId)
    {
        var foundDish = await originalDish.GetById(originalDishId);
        var foundTwist = twist.GetById(twistId);
        var collaborationSteps = recipe.CollaborateOnRecipeAsync(foundDish.Name, foundDish.Description, foundTwist.Description);
        await foreach(var collaborationStep in collaborationSteps)
        {
            yield return collaborationStep;
        }
    }
}
