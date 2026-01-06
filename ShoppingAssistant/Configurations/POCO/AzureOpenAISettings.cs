namespace ShoppingAssistant.Configurations.POCO;

/// <summary>
/// Provides configuration settings required to connect to Azure OpenAI services, including model selection, service
/// endpoint, and API key.
/// </summary>
/// <remarks>Use this class to specify the necessary parameters when initializing clients or services that
/// interact with Azure OpenAI. All properties must be set to valid, non-empty values to ensure successful
/// authentication and operation.</remarks>
public sealed class AzureOpenAISettings
{
    /// <summary>
    /// Gets or sets the name or identifier of the model associated with this instance.
    /// </summary>
    public string Model { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the network endpoint address used for connecting to the service.
    /// </summary>
    public string Endpoint { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the API key used to authenticate requests to external services.
    /// </summary>
    public string ApiKey { get; set; } = string.Empty;
}
