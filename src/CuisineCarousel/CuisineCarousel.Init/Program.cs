using CuisineCarousel.Init;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CuisineCarousel.Storage;

var builder = Host.CreateApplicationBuilder(args);
builder.AddStorage("tables", "blobs");

builder.Services.AddTransient<IInitHandler, InitAzureStorage>();
builder.Services.AddHostedService<MainHostedService>();

var host = builder.Build();
host.Run();