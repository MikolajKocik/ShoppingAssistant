using Microsoft.SemanticKernel;

namespace ShoppingAssistant.BuilderExtensionMembers.AzureOpenAI;

/// <summary>
/// Provides extension methods for configuring Azure OpenAI services within an ASP.NET Core application.
/// </summary>
public static class AzureOpenAIExtensions
{
    extension(WebApplicationBuilder builder)
    {
        /// <summary>
        /// Registers Azure OpenAI configuration settings with the application's dependency injection container.
        /// </summary>
        /// <returns>The current <see cref="WebApplicationBuilder"/> instance, enabling method chaining.</returns>
        public WebApplicationBuilder RegisterAzureOpenAIServices()
        {
            IConfigurationSection chatSettings =
                builder.Configuration.GetSection("AzureOpenAIChatSettings");

            IConfigurationSection embeddingSettings =
                builder.Configuration.GetSection("AzureOpenAIChatSettings");

            builder.Services.AddAzureOpenAIChatCompletion(
                deploymentName: chatSettings["Model"]!,
                endpoint: chatSettings["Endpoint"]!,
                apiKey: chatSettings["ApiKey"]!
            );

            builder.Services.AddAzureOpenAITextEmbeddingGeneration(
                       deploymentName: embeddingSettings["Model"]!,
                       endpoint: embeddingSettings["Endpoint"]!,
                       apiKey: embeddingSettings["ApiKey"]!
                   );

            return builder;
        }
    }
}
