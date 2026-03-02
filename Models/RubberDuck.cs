namespace RESTApi.Models;

/// <summary>
/// Represents a rubber duck collectible in the API.
/// </summary>
public class RubberDuck
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public string Size { get; set; } = "Medium"; // Small, Medium, Large
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}
