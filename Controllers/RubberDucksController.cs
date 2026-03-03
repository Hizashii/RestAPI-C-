using RESTApi.Data;
using RESTApi.DTOs;
using RESTApi.Models;

namespace RESTApi.Controllers;

/// <summary>
/// CRUD API for rubber duck collectibles.
/// All endpoints require authentication.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
[Produces("application/json")]
public class RubberDucksController : ControllerBase
{
    private readonly AppDbContext _context;

    public RubberDucksController(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Get all rubber ducks.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<RubberDuckResponseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<RubberDuckResponseDto>>> GetAll()
    {
        var ducks = await _context.RubberDucks
            .OrderBy(d => d.Id)
            .Select(d => MapToDto(d))
            .ToListAsync();
        return Ok(ducks);
    }

    /// <summary>
    /// Get a rubber duck by ID.
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(RubberDuckResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RubberDuckResponseDto>> GetById(int id)
    {
        var duck = await _context.RubberDucks.FindAsync(id);
        if (duck == null)
            return NotFound();

        return Ok(MapToDto(duck));
    }

    /// <summary>
    /// Create a new rubber duck.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(RubberDuckResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<RubberDuckResponseDto>> Create([FromBody] CreateRubberDuckDto dto)
    {
        var duck = new RubberDuck
        {
            Name = dto.Name,
            Color = dto.Color,
            Size = dto.Size,
            Price = dto.Price,
            Description = dto.Description
        };
        _context.RubberDucks.Add(duck);
        await _context.SaveChangesAsync();

        var response = MapToDto(duck);
        return CreatedAtAction(nameof(GetById), new { id = duck.Id }, response);
    }

    /// <summary>
    /// Update an existing rubber duck.
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(RubberDuckResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RubberDuckResponseDto>> Update(int id, [FromBody] UpdateRubberDuckDto dto)
    {
        var duck = await _context.RubberDucks.FindAsync(id);
        if (duck == null)
            return NotFound();

        if (dto.Name != null) duck.Name = dto.Name;
        if (dto.Color != null) duck.Color = dto.Color;
        if (dto.Size != null) duck.Size = dto.Size;
        if (dto.Price.HasValue) duck.Price = dto.Price.Value;
        if (dto.Description != null) duck.Description = dto.Description;
        duck.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return Ok(MapToDto(duck));
    }

    /// <summary>
    /// Delete a rubber duck.
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var duck = await _context.RubberDucks.FindAsync(id);
        if (duck == null)
            return NotFound();

        _context.RubberDucks.Remove(duck);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private static RubberDuckResponseDto MapToDto(RubberDuck d) => new()
    {
        Id = d.Id,
        Name = d.Name,
        Color = d.Color,
        Size = d.Size,
        Price = d.Price,
        Description = d.Description,
        CreatedAt = d.CreatedAt,
        UpdatedAt = d.UpdatedAt
    };
}
