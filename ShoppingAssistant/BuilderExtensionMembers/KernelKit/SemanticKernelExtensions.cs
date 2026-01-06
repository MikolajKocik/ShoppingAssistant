using Microsoft.SemanticKernel;
using ShoppingAssistant.Plugins;
using ShoppingAssistant.Services.Shopping;

namespace ShoppingAssistant.BuilderExtensionMembers.KernelKit;

/// <summary>
/// Provides extension methods for registering core e-commerce plugins and application services with the dependency
/// injection container in a web application.
/// </summary>
/// <remarks>Use the methods in this class to configure essential plugins and services during application startup.
/// These extensions enable plugin-based extensibility and ensure that all required services are available for
/// dependency injection and kernel composition throughout the application.</remarks>
public static class SemanticKernelExtensions
{
    extension(WebApplicationBuilder builder)
    {
        /// <summary>
        /// Registers core e-commerce plugins and related services with the application's dependency injection
        /// container.s
        /// </summary>
        /// <remarks>This method enables plugin-based extensibility by adding essential plugins, such as
        /// product search, cart, recommendations, user profiles, price tracking, and payment processing, to the
        /// application's service collection. Call this method during application startup to ensure all plugins are
        /// available for dependency injection and kernel composition.</remarks>
        /// <returns>The current <see cref="WebApplicationBuilder"/> instance with plugin services configured.</returns>
        public WebApplicationBuilder DefinePlugins()
        {
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

            return builder;
        }

        /// <summary>
        /// Registers core application services with the dependency injection container and returns the updated web
        /// application builder.
        /// </summary>
        /// <remarks>This method adds singleton registrations for essential services such as product
        /// catalog, cart, recommendations, user management, price tracking, and payment processing. Call this method
        /// during application startup to ensure all required services are available for dependency injection throughout
        /// the application.</remarks>
        /// <returns>The <see cref="WebApplicationBuilder"/> instance with the required services registered.</returns>
        public WebApplicationBuilder RegisterServices()
        {
            builder.Services.AddSingleton<ProductCatalogService>();
            builder.Services.AddSingleton<CartService>();
            builder.Services.AddSingleton<RecommendationService>();
            builder.Services.AddSingleton<UserService>();
            builder.Services.AddSingleton<PriceTrackerService>();
            builder.Services.AddSingleton<PaymentService>();

            return builder;
        }
    }
}
