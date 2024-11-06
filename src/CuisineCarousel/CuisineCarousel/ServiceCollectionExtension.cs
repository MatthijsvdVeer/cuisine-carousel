using CuisineCarousel.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace CuisineCarousel;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services.AddTransient<ICreateRecipeUseCase, CreateRecipeUseCase>();
        services.AddTransient<IUpdateRecipeUseCase, UpdateRecipeUseCase>();
        services.AddTransient<ICreateCollaborativeRecipeUseCase, CreateCollaborativeRecipeUseCase>();
        return services;
    }
}
