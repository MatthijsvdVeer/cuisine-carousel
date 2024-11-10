using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.Agents.Chat;

namespace CuisineCarousel.AzureAI;

internal sealed class ApprovalTerminationStrategy : TerminationStrategy
{
    protected override Task<bool> ShouldAgentTerminateAsync(Agent agent, IReadOnlyList<ChatMessageContent> history,
        CancellationToken cancellationToken)
        => Task.FromResult(
            history[^1].Content?.Contains("approve", StringComparison.OrdinalIgnoreCase) ?? false);
}
