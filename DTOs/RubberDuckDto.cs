using System.ComponentModel.DataAnnotations;

namespace RESTApi.DTOs;


public class CreateRubberDuckDto
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string Color { get; set; } = string.Empty;

    [StringLength(20)]
    public string Size { get; set; } = "Medium";

    [Range(0, 10000)]
    public decimal Price { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }
}


public class UpdateRubberDuckDto
{
    [StringLength(100)]
    public string? Name { get; set; }

    [StringLength(50)]
    public string? Color { get; set; }

    [StringLength(20)]
    public string? Size { get; set; }

    [Range(0, 10000)]
    public decimal? Price { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }
}


public class RubberDuckResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public string Size { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
