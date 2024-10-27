using CuisineCarousel.Models;

namespace CuisineCarousel.AzureAI;

internal sealed class RecipeService : IRecipe
{
    public Task<Recipe> CreateRecipeAsync(string originalDish, string twist)
    {
        return Task.FromResult(new Recipe("An Unholy Curry", "This curry is a mix of Indian and Thai flavors", "1. Mix the curry paste with the coconut milk\n2. Add the chicken and vegetables\n3. Simmer for 20 minutes"));
    }

    public Task<Recipe> UpdateRecipeAsync(Recipe originalDish, string instructions)
    {
        return Task.FromResult(new Recipe("The Holy Curry", "This Japanese curry will make you feel like you're in Tokyo", "1. Mix the curry paste with the sake\n2. Add the pork and prawns\n3. Simmer for 20 minutes"));
    }
}
