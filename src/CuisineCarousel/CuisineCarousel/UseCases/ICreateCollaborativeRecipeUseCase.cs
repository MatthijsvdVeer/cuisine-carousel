using CuisineCarousel.Models;

namespace CuisineCarousel.UseCases;

public interface ICreateCollaborativeRecipeUseCase
{
    IAsyncEnumerable<CollaborationStep> CreateRecipeAsync(string originalDishId, string twistId);
}