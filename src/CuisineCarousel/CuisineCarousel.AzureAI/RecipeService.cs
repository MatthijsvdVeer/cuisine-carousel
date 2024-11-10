using System.Text.Json;
using CuisineCarousel.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.PromptTemplates.Handlebars;
using Microsoft.SemanticKernel.PromptTemplates.Liquid;

namespace CuisineCarousel.AzureAI;

internal sealed class RecipeService(
    Kernel kernel,
    [FromKeyedServices(FunctionNames.CreateRecipe)]
    PromptTemplateConfig createRecipeConfig,
    [FromKeyedServices(FunctionNames.GordonRamsAI)]
    PromptTemplateConfig gordonRamsAiConfig,
    [FromKeyedServices(FunctionNames.ChatManager)]
    PromptTemplateConfig chatManagerConfig) : IRecipe
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new() { PropertyNameCaseInsensitive = true };

    public async Task<Recipe> CreateRecipeAsync(string originalDishName, string originalDishDescription, string twist)
    {
        var arguments = new KernelArguments
        {
            { "originalDishName", originalDishName },
            { "originalDishDescription", originalDishDescription },
            { "twist", twist }
        };

        var functionResult = await kernel.InvokeAsync(PluginNames.Prompty, FunctionNames.CreateRecipe, arguments);
        var recipeString = functionResult.GetValue<string>();
        var recipe = JsonSerializer.Deserialize<Recipe>(recipeString!,
            JsonSerializerOptions);
        if (recipe != null)
        {
            return recipe;
        }

        throw new InvalidOperationException("Failed to create recipe");
    }

    public async IAsyncEnumerable<CollaborationStep> CollaborateOnRecipeAsync(string originalDishName,
        string originalDishDescription, string twist)
    {
        var arguments = new KernelArguments
        {
            { "originalDishName", originalDishName },
            { "originalDishDescription", originalDishDescription },
            { "twist", twist }
        };

        ChatCompletionAgent recipeWriterAgent =
            new(createRecipeConfig,
                new AggregatorPromptTemplateFactory(new LiquidPromptTemplateFactory(),
                    new HandlebarsPromptTemplateFactory()))
            {
                Name = FunctionNames.CreateRecipe,
                Kernel = kernel,
                Arguments = new(arguments, createRecipeConfig.ExecutionSettings),
                LoggerFactory = kernel.LoggerFactory
            };


        ChatCompletionAgent agent =
            new(gordonRamsAiConfig,
                new AggregatorPromptTemplateFactory(new LiquidPromptTemplateFactory(),
                    new HandlebarsPromptTemplateFactory()))
            {
                Name = FunctionNames.GordonRamsAI,
                Kernel = kernel,
                Arguments = new(arguments, gordonRamsAiConfig.ExecutionSettings),
                LoggerFactory = kernel.LoggerFactory
            };
        
        ChatCompletionAgent managerAgent =
            new(chatManagerConfig,
                new AggregatorPromptTemplateFactory(new LiquidPromptTemplateFactory(),
                    new HandlebarsPromptTemplateFactory()))
            {
                Name = FunctionNames.ChatManager,
                Kernel = kernel,
                Arguments = new(gordonRamsAiConfig.ExecutionSettings.Select(pair => pair.Value)),
                LoggerFactory = kernel.LoggerFactory
            };

        AgentGroupChat chat =
            new(recipeWriterAgent, agent, managerAgent)
            {
                LoggerFactory = kernel.LoggerFactory,
                ExecutionSettings =
                    new()
                    {
                        TerminationStrategy =
                            new ApprovalTerminationStrategy()
                            {
                                Agents = [managerAgent],
                                MaximumIterations = 10,
                            }
                    }
            };

        await foreach (var content in chat.InvokeAsync())
        {
            switch (content.AuthorName)
            {
                case FunctionNames.CreateRecipe:
                    var recipe = JsonSerializer.Deserialize<Recipe>(content.Content!,
                        JsonSerializerOptions);
                    yield return new(content.AuthorName, null, recipe);
                    break;
                case FunctionNames.GordonRamsAI:
                    yield return new(content.AuthorName, content.Content, null);
                    break;
                case FunctionNames.ChatManager:
                    yield return new(content.AuthorName, content.Content, null);
                    break;
            }
        }
    }
}