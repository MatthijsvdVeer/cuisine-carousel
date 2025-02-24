using CuisineCarousel.Models;

namespace CuisineCarousel.UseCases;

public interface ICreateCollaborativeRecipeUseCase
{
    IAsyncEnumerable<CollaborationStep> CreateRecipeAsync(Guid originalDishId, string twistId);
}