using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var storage = builder.AddAzureStorage("storage")
    .RunAsEmulator(x => x
        .WithBlobPort(10020)
        .WithTablePort(10022)
        .WithImageTag("3.31.0")
    );

var tables = storage.AddTables("tables");
var blobs = storage.AddBlobs("blobs");

builder.AddProject<CuisineCarousel_Web>("cuisine-carousel-web")
    .WithReference(tables)
    .WithReference(blobs);


builder.AddProject<CuisineCarousel_Init>("cuisine-carousel-init")
    .WithReference(tables)
    .WithReference(blobs);

builder.Build().Run();