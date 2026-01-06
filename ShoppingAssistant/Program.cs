using ShoppingAssistant.Services.Kernel;
using ShoppingAssistant.BuilderExtensionMembers.VectorPg;
using ShoppingAssistant.BuilderExtensionMembers.AzureOpenAI;
using ShoppingAssistant.BuilderExtensionMembers.KernelKit;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.RegisterAzureOpenAIServices();

builder.DefinePlugins();
builder.RegisterServices();

builder.AddPostgresVectorStorage();
builder.DefineVectorStoreSchema();

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
