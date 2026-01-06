using ShoppingAssistant.Models.Enumerators;

namespace ShoppingAssistant.Models;

/// <summary>
/// Represents a product with identifying, descriptive, and categorization information, including brand, category,
/// price, rating, and description.
/// </summary>
/// <remarks>Use this class to model items in a catalog, inventory, or e-commerce system. All properties are
/// mutable, allowing updates to product details after creation. The class is sealed and cannot be inherited.</remarks>
public sealed class Product
{
    private Product() { } // EF

    /// <summary>
    /// Initializes a new instance of the Product class.
    /// </summary>
    public Product(
        string name,
        string brand,
        Category category,
        decimal price,
        double rating,
        string description
        )
    {
        this.Id = $"product_{Guid.CreateVersion7()}";

        this.Name = string.IsNullOrWhiteSpace(name)
            ? throw new ArgumentException("Name cannot be null or empty.", nameof(name))
            : name;

        this.Brand = string.IsNullOrWhiteSpace(brand)
            ? throw new ArgumentException("Brand cannot be null or empty.", nameof(brand))
            : brand;

        this.Category = category;

        this.Price = price >= 0
            ? price
            : throw new ArgumentOutOfRangeException(nameof(price), "Price cannot be negative.");

        this.Rating = rating >= 0 && rating <= 5
            ? rating
            : throw new ArgumentOutOfRangeException(nameof(rating), "Rating must be between 0 and 5.");

        this.Description = description ?? string.Empty;
    }

    /// <summary>
    /// Gets or sets the unique identifier for the entity.
    /// </summary>
    public string Id { get; private set; }

    /// <summary>
    /// Gets or sets the name associated with the object.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Gets or sets the brand name associated with the item.
    /// </summary>
    public string Brand { get; private set; }

    /// <summary>
    /// Gets or sets the category associated with the item.
    /// </summary>
    public Category Category { get; private set; }

    /// <summary>
    /// Gets or sets the price associated with the item.
    /// </summary>
    public decimal Price { get; private set; }


    /// <summary>
    /// Gets or sets the rating value associated with the item.
    /// </summary>
    public double Rating { get; private set; }

    /// <summary>
    /// Gets or sets the descriptive text associated with the object.
    /// </summary>
    public string Description { get; private set; }
}
