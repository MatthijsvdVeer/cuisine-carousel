using Microsoft.Extensions.Configuration;

namespace CuisineCarousel.AzureAI;

internal sealed class OpenAiConfiguration
{
    public string ApiUrl { get; }
    private OpenAiConfiguration(string apiUrl)
    {
        ApiUrl = apiUrl;
    }

    public static OpenAiConfiguration Get(IConfiguration config)
    {
        var openAiUrl = config["CUISINE_CAROUSEL_OPENAI_URL"] ?? throw new ApplicationException("Missing CUISINE_CAROUSEL_OPENAI_URL configuration value");
        return new(openAiUrl);
    }
}
