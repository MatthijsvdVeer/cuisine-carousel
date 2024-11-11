using CuisineCarousel;
using CuisineCarousel.AzureAI;
using CuisineCarousel.Observability;
using CuisineCarousel.Storage;
using CuisineCarousel.Web.Components;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

builder.AddOpenTelemetry("cuisine-carousel");

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddMudServices();
builder.Services
    .AddDomain()
    .AddStorage()
    .AddAzureAi(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
