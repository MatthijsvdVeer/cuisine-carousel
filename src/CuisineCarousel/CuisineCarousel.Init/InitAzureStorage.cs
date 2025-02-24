namespace CuisineCarousel.Init;

using Azure.Data.Tables;
using Models;
using Storage;

internal sealed class InitAzureStorage(
    TableServiceClient tableServiceClient,
    IOriginalDish originalDish)
    : IInitHandler
{
    private static readonly List<OriginalDish> OriginalDishes =
    [
        new(Guid.NewGuid(), "Spaghetti Carbonara", "A pasta dish made with eggs, cheese, bacon, and black pepper."),
        new(Guid.NewGuid(), "Chicken Tikka Masala", "A dish of roasted chicken chunks in a spicy sauce."),
        new(Guid.NewGuid(), "Beef Wellington", "A beef fillet coated with pâté and duxelles, which is then wrapped in puff pastry."),
        new(Guid.NewGuid(), "Sushi", "A dish of vinegared rice topped with raw fish or other ingredients."),
        new (Guid.NewGuid(), "Pad Thai", "A stir-fried rice noodle dish commonly served as a street food and at casual local eateries in Thailand."),
        new (Guid.NewGuid(), "Tacos", "A traditional Mexican dish consisting of a corn or wheat tortilla folded or rolled around a filling."),
        new (Guid.NewGuid(), "Cepelinai", "A traditional Lithuanian dish made from grated and riced potatoes and usually stuffed with ground meat.")
    ];

    public async Task RunAsync(CancellationToken cancellationToken)
    {
        await tableServiceClient.CreateTableIfNotExistsAsync(DishEntity.TableName, cancellationToken);
        await tableServiceClient.CreateTableIfNotExistsAsync(OriginalDishEntity.TableName, cancellationToken);
        foreach (var item in OriginalDishes)
        {
            await originalDish.Create(item);
        }
    }
}