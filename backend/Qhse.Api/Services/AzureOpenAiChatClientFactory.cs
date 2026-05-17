using System.ClientModel;
using Azure.AI.OpenAI;
using Microsoft.Extensions.Options;
using OpenAI.Chat;
using Qhse.Api.Options;

namespace Qhse.Api.Services;

public sealed class AzureOpenAiChatClientFactory(IOptions<AzureOpenAiOptions> options) : IAzureOpenAiChatClientFactory
{
    public string DeploymentName => GetValidatedOptions().DeploymentName;

    public ChatClient CreateClient()
    {
        var azureOptions = GetValidatedOptions();
        var azureClient = new AzureOpenAIClient(
            new Uri(azureOptions.Endpoint),
            new ApiKeyCredential(azureOptions.ApiKey));

        return azureClient.GetChatClient(azureOptions.DeploymentName);
    }

    private AzureOpenAiOptions GetValidatedOptions()
    {
        var azureOptions = options.Value;

        if (string.IsNullOrWhiteSpace(azureOptions.Endpoint))
        {
            throw new InvalidOperationException("Azure OpenAI endpoint is not configured. Set AzureOpenAI__Endpoint.");
        }

        if (string.IsNullOrWhiteSpace(azureOptions.ApiKey))
        {
            throw new InvalidOperationException("Azure OpenAI API key is not configured. Set AzureOpenAI__ApiKey.");
        }

        if (string.IsNullOrWhiteSpace(azureOptions.DeploymentName))
        {
            throw new InvalidOperationException("Azure OpenAI deployment name is not configured. Set AzureOpenAI__DeploymentName.");
        }

        return azureOptions;
    }
}
