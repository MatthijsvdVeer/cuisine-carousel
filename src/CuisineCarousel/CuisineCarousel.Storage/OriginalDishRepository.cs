using CuisineCarousel.Models;

namespace CuisineCarousel.Storage;

using Azure;
using Azure.Data.Tables;
using Microsoft.Extensions.Logging;

internal sealed class OriginalDishRepository(
    TableServiceClient tableServiceClient,
    ILogger<OriginalDishRepository> logger)
    : IOriginalDish
{
    private readonly TableClient tableClient = tableServiceClient.GetTableClient(OriginalDishEntity.TableName);

    public async Task<OriginalDish> GetById(Guid id)
    {
        var response = await this.tableClient.GetEntityAsync<OriginalDishEntity>(
            OriginalDishEntity.FormatKey(id),
            OriginalDishEntity.FormatKey(id));
        return response.Value.ToOriginalDish();
    }

    public async Task<IEnumerable<OriginalDish>> GetAll()
    {
        var asyncPageable = this.tableClient.QueryAsync<OriginalDishEntity>();
        var dishes = new List<OriginalDish>();
        await foreach (var dishEntity in asyncPageable)
        {
            dishes.Add(dishEntity.ToOriginalDish());
        }

        return dishes;
    }

    public async Task Create(OriginalDish originalDish)
    {
        try
        {
            var tableEntity = OriginalDishEntity.FromOriginalDish(originalDish);
            _ = await this.tableClient.AddEntityAsync(tableEntity);
        }
        catch (RequestFailedException exception)
        {
            logger.LogError(exception, "Failed to save dish");
            throw;
        }
    }
}