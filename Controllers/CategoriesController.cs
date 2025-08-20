using Ecommerce.API.Data;
using Ecommerce.API.DTOs.Category;
using Ecommerce.API.Mapping;
using Ecommerce.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Ecommerce.API.Controllers;

[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly EcommerceDbContext _dbContext;

    public CategoriesController(EcommerceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("api/categories")]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _dbContext   .Categories.AsNoTracking().Select(category => category.ToResponseDto()).ToListAsync();
        return Ok(categories);
    }
    
    [HttpGet("api/categories/{id:guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var category = await _dbContext.Categories.AsNoTracking().SingleOrDefaultAsync(c => c.Id == id);
        if (category is  null)
        {
            return NotFound();
        }
        return Ok(category.ToResponseDto());
    }

    [HttpPost("api/categories")]
    public async Task<IActionResult> Create([FromBody] CreateCategoryDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var category = new Category
        {
            Name = dto.Name
        };
        await _dbContext.Categories.AddAsync(category);
        await _dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(Get), new { id = category.Id }, category.ToResponseDto());
    }

    [HttpPut("api/categories/{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateCategoryDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var category = await _dbContext.Categories.SingleOrDefaultAsync(x => x.Id == id);
        if( category is null)
        {
            return NotFound();
        }
        category.Name = dto.Name;
        await _dbContext.SaveChangesAsync();

        return NoContent();
    }
    
    [HttpDelete("api/categories/{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var category = await _dbContext.Categories.SingleOrDefaultAsync(x => x.Id == id);
        if( category is null)
        {
            return NotFound();
        }
        _dbContext.Categories.Remove(category);
        await _dbContext.SaveChangesAsync();

        return NoContent();
    }
}