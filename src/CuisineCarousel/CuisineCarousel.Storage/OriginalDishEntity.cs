namespace CuisineCarousel.Storage;

using Azure;
using Azure.Data.Tables;
using Models;

public sealed class OriginalDishEntity : ITableEntity
{
    public const string TableName = "originalDishes";
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string PartitionKey { get; set; }
    public string RowKey { get; set; }
    public DateTimeOffset? Timestamp { get; set; }
    public ETag ETag { get; set; }

    public static OriginalDishEntity FromOriginalDish(OriginalDish originalDish) =>
        new()
        {
            Id = originalDish.Id,
            Timestamp = DateTimeOffset.UtcNow,
            PartitionKey = FormatKey(originalDish.Id),
            RowKey = FormatKey(originalDish.Id),
            Name = originalDish.Name,
            Description = originalDish.Description
        };

    public static string FormatKey(Guid id) => id.ToString("D");

    public OriginalDish ToOriginalDish() => new(this.Id, this.Name, this.Description);
}