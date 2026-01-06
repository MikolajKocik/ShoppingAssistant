using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.PromptTemplates.Handlebars;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace ShoppingAssistant.Services.Kernel;

/// <summary>
/// Provides functionality to load and access the shopping assistant prompt configuration from a YAML file.
/// </summary>
/// <remarks>The prompt configuration is expected to be located at 'Prompts/shopping-assistant.yml' relative to
/// the application's base directory. Ensure that the required YAML file is present before creating an instance of this
/// class. This class is sealed and cannot be inherited.</remarks>
public sealed class PromptLoader
{
    private readonly KernelFunction _shoppingAssistantPrompt;

    /// <summary>
    /// Initializes a new instance of the PromptLoader class and loads the shopping assistant prompt configuration from
    /// a YAML file.
    /// </summary>
    /// <remarks>The constructor expects the prompt configuration file to be located at
    /// 'Prompts/shopping-assistant.yml' relative to the application's base directory. Ensure that the required YAML
    /// file is present before creating an instance of this class.</remarks>
    /// <exception cref="FileNotFoundException">Thrown if the shopping assistant YAML prompt file does not exist in the expected location.</exception>
    public PromptLoader()
    {
        string yamlPath = Path.Combine(AppContext.BaseDirectory, "Prompts", "shopping-assistant.yml");

        if (!File.Exists(yamlPath))
        {
            throw new FileNotFoundException($"YAML prompt file not found at path: {yamlPath}");
        }

        string yamlContent = File.ReadAllText(yamlPath);

        var deserializer = new DeserializerBuilder()
           .WithNamingConvention(UnderscoredNamingConvention.Instance)
           .Build();

        PromptTemplateConfig config = deserializer.Deserialize<PromptTemplateConfig>(yamlContent);

        var factory = new HandlebarsPromptTemplateFactory();

        IPromptTemplate template = factory.Create(config);

        this._shoppingAssistantPrompt = KernelFunctionFactory.CreateFromPrompt(template, config);
    }

    /// <summary>
    /// Gets the kernel function that provides the shopping assistant prompt.
    /// </summary>
    /// <returns>A <see cref="KernelFunction"/> instance representing the shopping assistant prompt.</returns>
    public KernelFunction GetShoppingAssistantPrompt()
        => this._shoppingAssistantPrompt;
}
