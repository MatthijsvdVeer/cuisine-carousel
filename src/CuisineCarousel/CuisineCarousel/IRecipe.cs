using CuisineCarousel.Models;

namespace CuisineCarousel;

public interface IRecipe
{
    Task<Recipe> CreateRecipeAsync(string originalDishName, string originalDishDescription, string twist);
    
    Task<Recipe> UpdateRecipeAsync(Recipe recipe, string instructions);
    IAsyncEnumerable<CollaborationStep> CollaborateOnRecipeAsync(string originalDishName, string originalDishDescription, string twist);
}
