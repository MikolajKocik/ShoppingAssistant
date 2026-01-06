namespace ShoppingAssistant.Requests;

/// <summary>
/// Gets or sets the text content of the user message.
/// </summary>
public sealed class UserMessage
{
    /// <summary>
    /// Gets or sets the message text associated with this instance.
    /// </summary>
    public string Message { get; set; } = string.Empty;
}
