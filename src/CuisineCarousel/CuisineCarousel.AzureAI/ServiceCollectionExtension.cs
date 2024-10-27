using Microsoft.Extensions.DependencyInjection;

namespace CuisineCarousel.AzureAI;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddAzureAI(this IServiceCollection services)
    {
        services.AddTransient<IRecipe, RecipeService>();
        return services;
    }
}
