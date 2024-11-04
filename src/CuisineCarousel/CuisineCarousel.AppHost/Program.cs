using Projects;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<CuisineCarousel_Web>("cuisine-carousel-web");

builder.Build().Run();