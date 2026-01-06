using Microsoft.SemanticKernel;
using ShoppingAssistant.Configurations.POCO;
using ShoppingAssistant.Plugins;
using ShoppingAssistant.Services.Kernel;
using ShoppingAssistant.Services.Shopping;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<AzureOpenAISettings>(
    builder.Configuration.GetSection("AzureOpenAISettings"));

IConfigurationSection azureOpenAISettings = builder.Configuration.GetSection("AzureOpenAISettings");

builder.Services.AddAzureOpenAIChatCompletion(
    deploymentName: azureOpenAISettings["Model"]!,
    endpoint: azureOpenAISettings["Endpoint"]!,
    apiKey: azureOpenAISettings["ApiKey"]!
);

// Register services
builder.Services.AddSingleton<ProductCatalogService>();
builder.Services.AddSingleton<CartService>();
builder.Services.AddSingleton<RecommendationService>();
builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<PriceTrackerService>();
builder.Services.AddSingleton<PaymentService>();

// Register plugins
builder.Services.AddSingleton<ProductSearchPlugin>();
builder.Services.AddSingleton<CartPlugin>();
builder.Services.AddSingleton<RecommendationPlugin>();
builder.Services.AddSingleton<UserProfilePlugin>();
builder.Services.AddSingleton<PriceTrackerPlugin>();
builder.Services.AddSingleton<PaymentPlugin>();

// Create plugin collection
builder.Services.AddSingleton<KernelPluginCollection>(sp =>
{
    return
    [
        KernelPluginFactory.CreateFromObject(sp.GetRequiredService<ProductSearchPlugin>()),
        KernelPluginFactory.CreateFromObject(sp.GetRequiredService<CartPlugin>()),
        KernelPluginFactory.CreateFromObject(sp.GetRequiredService<RecommendationPlugin>()),
        KernelPluginFactory.CreateFromObject(sp.GetRequiredService<UserProfilePlugin>()),
        KernelPluginFactory.CreateFromObject(sp.GetRequiredService<PriceTrackerPlugin>()),
        KernelPluginFactory.CreateFromObject(sp.GetRequiredService<PaymentPlugin>())
    ];
});

builder.Services.AddTransient(sp =>
{
    var plugins = sp.GetRequiredService<KernelPluginCollection>();
    return new Kernel(sp, plugins);
});

builder.Services.AddSingleton<PromptLoader>();

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
