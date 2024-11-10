using System.ClientModel;
using Azure;
using Azure.AI.OpenAI;
using Azure.Identity;
using CuisineCarousel.Models;
using Microsoft.SemanticKernel.TextToImage;
using OpenAI.Images;

namespace CuisineCarousel.AzureAI;

internal sealed class ImageFabricator(ITextToImageService textToImageService) : IFabricator
{
    public async Task Fabricate(Recipe recipe)
    {
        /*// step 1 is to create the prompt with our new prompt creation function
        var prompt = "A picture of deep-fried pasta carbonara, served in a wicker basket. In the style of 1950 sci-fi retro comic book.";

        // step 2 is to gen the image with that prompt
        var image = await textToImageService.GenerateImageAsync(prompt, 1024, 1024);

        return;*/
        

        AzureOpenAIClient client = new(new Uri("https://oai-cuisine-swe.openai.azure.com/"), new ApiKeyCredential("nope"));
        var imageClient = client.GetImageClient("dall-e-3");
        var foo = await imageClient.GenerateImageAsync("A picture of deep-fried pasta carbonara, served in a wicker basket. In the style of 1950 sci-fi retro comic book.");

        return;
    }
}
