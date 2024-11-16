namespace CuisineCarousel.Storage;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public static class ServiceCollectionExtension
{
    public static IHostApplicationBuilder AddStorage(this IHostApplicationBuilder builder, string tablesConnectionName,
        string blobsConnectionName)
    {
        builder.Services
            .AddSingleton<IOriginalDish, OriginalDishRepository>()
            .AddSingleton<ITwist, TwistRepository>()
            .AddTransient<IDish, DishRepository>();
        builder.AddAzureTableClient(tablesConnectionName);
        builder.AddAzureBlobClient(blobsConnectionName);
        return builder;
    }
}