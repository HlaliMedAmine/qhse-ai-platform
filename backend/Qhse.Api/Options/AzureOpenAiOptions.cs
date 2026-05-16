namespace Qhse.Api.Options;

public sealed class AzureOpenAiOptions
{
    public string Endpoint { get; set; } = string.Empty;
    public string DeploymentName { get; set; } = string.Empty;
    public string ApiKey { get; set; } = string.Empty;
    public bool UseMock { get; set; } = true;
}
