using System.ClientModel;
using Azure;
using Azure.AI.OpenAI;
using Azure.Identity;
using CuisineCarousel.Models;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.TextToImage;
using OpenAI.Images;

namespace CuisineCarousel.AzureAI;

internal sealed class ImageFabricator(ITextToImageService textToImageService, Kernel kernel) : IFabricator
{
    public async Task<Uri> Fabricate(Recipe recipe)
    {
        var arguments = new KernelArguments
        {
            { "recipeTitle", recipe.Title },
            { "recipeDescription", recipe.Description },
            { "recipeInstructions", recipe.Instructions }
        };
        
        var functionResult = await kernel.InvokeAsync(PluginNames.Prompty, FunctionNames.CreateImage, arguments);
        var image = await textToImageService.GenerateImageAsync(functionResult.GetValue<string>(), 1024, 1024);
        return new(image);
    }
}