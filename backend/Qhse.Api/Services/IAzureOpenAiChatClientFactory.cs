using OpenAI.Chat;

namespace Qhse.Api.Services;

public interface IAzureOpenAiChatClientFactory
{
    string DeploymentName { get; }
    ChatClient CreateClient();
}
