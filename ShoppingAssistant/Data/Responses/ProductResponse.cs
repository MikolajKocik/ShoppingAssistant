namespace ShoppingAssistant.Data.Responses;

/// <summary>
/// Represents the data returned for a product, including identification, descriptive, and pricing information.
/// </summary>
/// <remarks>This record is typically used as a data transfer object in API responses to encapsulate product
/// details. All properties are immutable and set during object initialization.</remarks>
public record ProductResponse
{
    public string Id { get; init; }

    public string Name { get; init; }

    public string Brand { get; init; }

    public string Category { get; init; }

    public decimal Price { get; init; }

    public double Rating { get; init; }
    public string Description { get; init; }
}
