using Azure.Data.Tables;
using Azure.Storage.Blobs;
using CuisineCarousel.Models;

namespace CuisineCarousel.Storage;

using Azure;
using Microsoft.Extensions.Logging;

internal sealed class DishRepository(TableServiceClient tableServiceClient, BlobServiceClient blobServiceClient, ILogger<DishRepository> logger) : IDish
{
    private readonly TableClient tableClient = tableServiceClient.GetTableClient(DishEntity.TableName);

    public async Task<Dish> SaveAsync(Recipe recipe, Uri imageLocation)
    {
        try
        {
            var dish = new Dish(recipe, imageLocation.ToString());
            var dishEntity = DishEntity.FromDish(dish);
            _ = await this.tableClient.AddEntityAsync(dishEntity);
            return dish;
        }
        catch (RequestFailedException exception)
        {
            logger.LogError(exception, "Failed to save dish");
            throw;
        }
    }

    public async Task<IEnumerable<Dish>> GetAllAsync()
    {
        var asyncPageable = this.tableClient.QueryAsync<DishEntity>();
        var dishes = new List<Dish>();
        await foreach (var dishEntity in asyncPageable)
        {
            dishes.Add(dishEntity.ToDish());
        }

        return dishes;
    }
}