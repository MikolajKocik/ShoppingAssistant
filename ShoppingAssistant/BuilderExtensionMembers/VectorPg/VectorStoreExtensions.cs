using Azure;
using Azure.AI.OpenAI;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.VectorData;
using ShoppingAssistant.Models;

namespace ShoppingAssistant.BuilderExtensionMembers.VectorPg;

/// <summary>
/// Provides extension methods for configuring vector storage and schema definitions in a web application builder.
/// </summary>
/// <remarks>These extension methods enable integration of vector database functionality and schema setup within
/// ASP.NET Core applications. Use these methods to register vector storage services and define collection schemas for
/// vector-based data operations.</remarks>
public static class VectorStoreExtensions
{
    extension(WebApplicationBuilder builder)
    {
        /// <summary>
        /// Configures the application to use PostgreSQL as the vector storage backend.
        /// </summary>
        /// <remarks>This method retrieves the connection string named "VectorDB" from the application's
        /// configuration and registers the PostgreSQL vector store service. Ensure that the connection string is
        /// defined before calling this method.</remarks>
        /// <returns>The current <see cref="WebApplicationBuilder"/> instance, enabling method chaining.</returns>
        public WebApplicationBuilder AddPostgresVectorStorage()
        {
            builder.Services.AddPostgresVectorStore(builder.Configuration.GetConnectionString("VectorDB")!);
            return builder;
        }

        /// <summary>
        /// Configures and registers a vector store collection schema for products, including properties such as Id,
        /// Name, Brand, Category, Price, Rating, and Description.
        /// </summary>
        /// <remarks>This method adds a singleton vector store collection for products to the
        /// application's dependency injection container. The schema defines which product properties are indexed or
        /// full-text indexed, enabling efficient querying and retrieval. Call this method during application startup to
        /// ensure the collection is available for use throughout the application.</remarks>
        /// <returns>The current <see cref="WebApplicationBuilder"/> instance, enabling further configuration of the application.</returns>
        public WebApplicationBuilder DefineVectorStoreSchema()
        {
            var productDefinition = new VectorStoreCollectionDefinition
            {
                Properties = new List<VectorStoreProperty>
                {
                    new VectorStoreKeyProperty("Id", typeof(string)),
                    new VectorStoreDataProperty("Name", typeof(string)) { IsIndexed = true },
                    new VectorStoreDataProperty("Brand", typeof(string)) { IsIndexed = true },
                    new VectorStoreDataProperty("Category", typeof(string)) { IsIndexed = true },
                    new VectorStoreDataProperty("Price", typeof(decimal)) { IsIndexed = true },
                    new VectorStoreDataProperty("Rating", typeof(double)) { IsIndexed = true },
                    new VectorStoreDataProperty("Description", typeof(string)) { IsFullTextIndexed = true }
                }
            };

            builder.Services.AddSingleton(sp =>
            {
                var vectorStore = sp.GetRequiredService<VectorStore>();
                return vectorStore.GetCollection<string, Product>("products", productDefinition);
            });

            return builder;
        }
    }
}
