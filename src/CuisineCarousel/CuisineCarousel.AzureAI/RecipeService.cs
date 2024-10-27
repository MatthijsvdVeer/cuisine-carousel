using CuisineCarousel.Models;
using Microsoft.SemanticKernel;

namespace CuisineCarousel.AzureAI;

internal sealed class RecipeService(Kernel kernel) : IRecipe
{
    public async Task<Recipe> CreateRecipeAsync(string originalDishName, string originalDishDescription, string twist)
    {
        var arguments = new KernelArguments()
        {
            { "originalDishName", originalDishName },
            { "originalDishDescription", originalDishDescription },
            { "twist", twist }
        };

        try
        {
            var functionResult = await kernel.InvokeAsync(PluginNames.Prompty, FunctionNames.CreateRecipe, arguments);
            var recipe = functionResult.GetValue<Recipe>();
            if (recipe != null)
            {
                return recipe;
            }
        }
        catch (HttpOperationException exception)
        {
            
            throw;
        }

        throw new InvalidOperationException("Failed to create recipe");
    }

    public Task<Recipe> UpdateRecipeAsync(Recipe recipe, string instructions)
    {
        return Task.FromResult(new Recipe("The Holy Curry",
            "This Japanese curry will make you feel like you're in Tokyo",
            "1. Mix the curry paste with the sake\n2. Add the pork and prawns\n3. Simmer for 20 minutes"));
    }
}
