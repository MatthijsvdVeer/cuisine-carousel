using System.Text.Json;
using CuisineCarousel.Models;
using Microsoft.SemanticKernel;

namespace CuisineCarousel.AzureAI;

internal sealed class RecipeService(Kernel kernel) : IRecipe
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new() { PropertyNameCaseInsensitive = true };

    public async Task<Recipe> CreateRecipeAsync(string originalDishName, string originalDishDescription, string twist)
    {
        var arguments = new KernelArguments
        {
            { "originalDishName", originalDishName },
            { "originalDishDescription", originalDishDescription },
            { "twist", twist }
        };

        var functionResult = await kernel.InvokeAsync(PluginNames.Prompty, FunctionNames.CreateRecipe, arguments);
        var recipeString = functionResult.GetValue<string>();
        var recipe = JsonSerializer.Deserialize<Recipe>(recipeString!,
            JsonSerializerOptions);
        if (recipe != null)
        {
            return recipe;
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