namespace CuisineCarousel.Init;

using Azure.Data.Tables;
using Storage;

internal sealed class InitAzureStorage(
    TableServiceClient tableServiceClient)
    : IInitHandler
{
    public async Task RunAsync(CancellationToken cancellationToken) =>
        await tableServiceClient.CreateTableIfNotExistsAsync(DishEntity.TableName, cancellationToken);
}