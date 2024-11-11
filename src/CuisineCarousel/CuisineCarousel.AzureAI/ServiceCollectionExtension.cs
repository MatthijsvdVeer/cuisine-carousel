using System.Reflection;
using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Prompty;

namespace CuisineCarousel.AzureAI;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddAzureAi(this IServiceCollection services, IConfiguration configuration)
    {
        // Materializing the services here allows us to resolve ILoggerFactory.
        // This is necessary for the Prompty functions to log.
        // This gives us access to the rendered prompt.
        var loggerFactory = services.BuildServiceProvider().GetService<ILoggerFactory>();

        var config = OpenAiConfiguration.Get(configuration);
        services
            .AddAzureOpenAIChatCompletion(ModelNames.Gpt4o, config.ApiUrl, new DefaultAzureCredential(), apiVersion: "2024-08-01-preview", modelId: ModelNames.Gpt4o)
            .AddAzureOpenAIChatCompletion(ModelNames.Gpt35, config.ApiUrl, new DefaultAzureCredential(), apiVersion: "2024-08-01-preview", modelId: ModelNames.Gpt35)
            .AddAzureOpenAIChatCompletion(ModelNames.Gpt4oMini, config.ApiUrl, new DefaultAzureCredential(), apiVersion: "2024-08-01-preview", modelId: ModelNames.Gpt4oMini)
            .AddAzureOpenAITextToImage(ModelNames.DallE3, config.ApiUrl, new DefaultAzureCredential(), apiVersion: "2024-06-01", modelId: ModelNames.DallE3);

        var kernelBuilder = services.AddKernel();
        kernelBuilder.Plugins.AddPromptyFunctions(loggerFactory);
        services.AddPromptyTemplates();
        services.AddTransient<IRecipe, RecipeService>();
        services.AddTransient<IFabricator, ImageFabricator>();
        return services;
    }

   private static IKernelBuilderPlugins AddPromptyFunctions(this IKernelBuilderPlugins kernelPlugins,
       ILoggerFactory? factory)
    {
        var functions = CreatePromptyFunctions(factory).ToArray();

        return kernelPlugins.AddFromFunctions(PluginNames.Prompty, functions);
    }

    private static IEnumerable<KernelFunction> CreatePromptyFunctions(ILoggerFactory? factory)
    {
        var basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var promptyDirectory = Path.Join(basePath, "Prompts");
        foreach (var file in Directory.EnumerateFiles(promptyDirectory, "*.prompty"))
        {
            yield return CreatePromptyFunction(file, factory);
        }
    }

    private static KernelFunction CreatePromptyFunction(string fileName, ILoggerFactory? factory)
    {
        var text = File.ReadAllText(fileName);
        return KernelFunctionPrompty.FromPrompty(text, loggerFactory: factory);
    }
    
    private static IServiceCollection AddPromptyTemplates(this IServiceCollection services)
    {
        var basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var promptyDirectory = Path.Join(basePath, "Prompts");
        foreach (var file in Directory.EnumerateFiles(promptyDirectory, "*.prompty"))
        {
            var promptTemplateConfig = CreatePromptyTemplateConfig(file);
            services.AddKeyedSingleton(promptTemplateConfig.Name, promptTemplateConfig);
        }
        
        return services;
    }
    
    private static PromptTemplateConfig CreatePromptyTemplateConfig(string fileName)
    {
        var text = File.ReadAllText(fileName);
        return KernelFunctionPrompty.ToPromptTemplateConfig(text);
    }
}
