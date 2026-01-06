using System.ComponentModel;
using Microsoft.SemanticKernel;
using ShoppingAssistant.Data.Responses;
using ShoppingAssistant.Models.Enumerators;

namespace ShoppingAssistant.Plugins;

/// <summary>
/// Provides methods for searching, filtering, sorting, and retrieving detailed information about products based on
/// various criteria.
/// </summary>
/// <remarks>The ProductSearchPlugin class exposes asynchronous operations to help clients discover and organize
/// products according to name, category, brand, price, popularity, and rating. All methods are designed to be used in
/// asynchronous workflows and do not block the calling thread. This class is intended for integration into systems that
/// require flexible product discovery and management capabilities.</remarks>
public sealed class ProductSearchPlugin
{
    /// <summary>
    /// Asynchronously searches for products that match the specified name, category, brand, and additional keywords.
    /// </summary>
    /// <param name="name">The name or partial name of the product to search for. Can be an empty string to match any name.</param>
    /// <param name="category">The category to filter products by. Only products in this category are included in the results.</param>
    /// <param name="brand">The brand to filter products by. Only products from this brand are included in the results.</param>
    /// <param name="keywords">Additional keywords to refine the search. Products matching any of these keywords are included in the results.
    /// Can be omitted if not needed.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [KernelFunction("search_products")]
    [Description("Search products based on name, category, brand and additional keywords")]
    public async Task<IEnumerable<ProductResponse>> SearchProductsAsync(
        [Description("Name of product")] string name,
        [Description("Product category")] Category category,
        [Description("Product brand")] string brand,
        [Description("Additional unique keywords to search products")] params string[] keywords
        )
    {
        return [];
    }

    /// <summary>
    /// Asynchronously filters products based on the specified minimum price, maximum price, brand, and category.
    /// </summary>
    /// <param name="minPrice">The minimum price of products to include in the results. Must be less than or equal to <paramref
    /// name="maxPrice"/>.</param>
    /// <param name="maxPrice">The maximum price of products to include in the results. Must be greater than or equal to <paramref
    /// name="minPrice"/>.</param>
    /// <param name="brand">The brand to filter products by. Only products matching this brand are included.</param>
    /// <param name="category">The category to filter products by. Only products within this category are included.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [KernelFunction("filter_products")]
    [Description("Filter products based on minimal price, max price, brand and category")]
    public async Task<IEnumerable<ProductResponse>> FilterProductsAsync(
        [Description("Product minimal price")] decimal minPrice,
        [Description("Product maksimum price")] decimal maxPrice,
        [Description("Product brand")] string brand,
        [Description("Product category")] Category category
        )
    {
        return [];
    }

    /// <summary>
    /// Sorts products asynchronously based on the specified criteria, including price increase, price decrease,
    /// popularity, and rating.
    /// </summary>
    /// <param name="priceIncrease">The weight to apply when sorting by increasing price. Higher values give greater priority to products with
    /// higher prices.</param>
    /// <param name="priceDecrease">The weight to apply when sorting by decreasing price. Higher values give greater priority to products with lower
    /// prices.</param>
    /// <param name="popularity">The weight to apply when sorting by product popularity. Higher values give greater priority to more popular
    /// products.</param>
    /// <param name="rating">The weight to apply when sorting by product rating. Higher values give greater priority to products with higher
    /// ratings.</param>
    /// <returns>A task that represents the asynchronous sort operation.</returns>
    [KernelFunction("sort_products")]
    [Description("Sort products based on price increase, price decrease, popularity and rating")]
    public async Task<IEnumerable<ProductResponse>> SortProductsAsync(
        [Description("Product sorted by price increase")] decimal priceIncrease,
        [Description("Product sorted by price decrease")] decimal priceDecrease,
        [Description("Product popularity")] int popularity,
        [Description("Rating for product")] double rating)
    {
        return [];
    }

    /// <summary>
    /// Asynchronously retrieves detailed information about a specific product using its unique identifier.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [KernelFunction("get_product_details")]
    [Description("Get detailed information about a specific product by its unique identifier")]
    public async Task<ProductResponse> GetProductDetailsAsync()
    {
        return null!;
    }
}
