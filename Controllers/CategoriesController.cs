using Ecommerce.API.Data;
using Ecommerce.API.Mapping;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
}