using Microsoft.Extensions.DependencyInjection;

namespace CuisineCarousel.Storage;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddStorage(this IServiceCollection services)
    {
        services.AddSingleton<IOriginalDish, OriginalDishRepository>();
        services.AddSingleton<ITwist, TwistRepository>();
        return services;
    }
}
