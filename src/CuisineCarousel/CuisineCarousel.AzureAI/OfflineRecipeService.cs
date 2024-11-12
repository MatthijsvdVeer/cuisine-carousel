using CuisineCarousel.Models;

namespace CuisineCarousel.AzureAI;

internal sealed class OfflineRecipeService : IRecipe
{
    private static readonly Recipe recipe = new Recipe("Pad Thai Eiffel Tower", "A twist on the classic Pad Thai, inspired by the Eiffel Tower", "1. Boil water. 2. Add noodles. 3. Stir in sauce. 4. Garnish with Eiffel Tower-shaped carrots.");

    public Task<Recipe> CreateRecipeAsync(string originalDishName, string originalDishDescription, string twist)
    {
        return Task.FromResult(
            recipe);
    }

    public async IAsyncEnumerable<CollaborationStep> CollaborateOnRecipeAsync(string originalDishName, string originalDishDescription, string twist)
    {
        var steps = new List<CollaborationStep>
        {
            new("CreateRecipe", null, recipe),
            new("GordonRamsAI", "You donkey!", null),
            new("CreateRecipe", null, recipe),
            new("GordonRamsAI", "You donkey!", null),
            new("CreateRecipe", null, recipe),
            new("GordonRamsAI", "You donkey!", null),
        };

        foreach (var step in steps)
        {
            await Task.Delay(2500);
            yield return step;
        }
    }
}