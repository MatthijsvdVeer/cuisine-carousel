using Azure;
using Azure.Data.Tables;
using CuisineCarousel.Models;

namespace CuisineCarousel.Storage;

public sealed class DishEntity : ITableEntity
{
    public const string TableName = "fabricatedDishes";
    
    public string Name { get; set; }
    public string Description { get; set; }
    public string Instructions { get; set; }
    public string PartitionKey { get; set; }
    public string ImageBlobLocation { get; set; }
    public string RowKey { get; set; }
    public DateTimeOffset? Timestamp { get; set; }
    
    public ETag ETag { get; set; }
    
    public static DishEntity FromDish(Dish dish)
    {
        // TODO: This should become a proper id, likely a user ID.
        var id = Guid.NewGuid().ToString();
        return new()
        {
            Name = dish.Recipe.Title,
            Description = dish.Recipe.Description,
            Instructions = dish.Recipe.Instructions,
            ImageBlobLocation = dish.ImageBlobLocation,
            PartitionKey = id,
            RowKey = id,
            Timestamp = DateTimeOffset.UtcNow
        };
    }
    
    public Dish ToDish()
    {
        return new(new(Name, Description, Instructions), ImageBlobLocation);
    }
}