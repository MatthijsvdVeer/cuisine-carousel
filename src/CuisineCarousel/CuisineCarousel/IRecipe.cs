using CuisineCarousel.Models;

namespace CuisineCarousel;

public interface IRecipe
{
    Task<Recipe> CreateRecipeAsync(string originalDish, string twist);
    
    Task<Recipe> UpdateRecipeAsync(Recipe originalDish, string instructions);
}
