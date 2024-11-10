using CuisineCarousel.Models;

namespace CuisineCarousel;

public interface IRecipe
{
    Task<Recipe> CreateRecipeAsync(string originalDishName, string originalDishDescription, string twist);
    
    IAsyncEnumerable<CollaborationStep> CollaborateOnRecipeAsync(string originalDishName, string originalDishDescription, string twist);
}
