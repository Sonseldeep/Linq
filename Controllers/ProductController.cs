using Ecommerce.API.Data;
using Ecommerce.API.DTOs.Product;
using Ecommerce.API.Mapping;
using Ecommerce.API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Controllers;

public class ProductController : ControllerBase
{
    private readonly EcommerceDbContext _dbContext;

    public ProductController(EcommerceDbContext dbContext )
    {
        _dbContext = dbContext;
    }

    [HttpGet("api/products")]
    public async Task<IActionResult> GetAll(
        [FromQuery] Guid? categoryId,
        [FromQuery] string? search,
        [FromQuery] string? sortByPrice,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var query = _dbContext.Products
            .AsNoTracking()
            .Include(p => p.Category)
            .AsQueryable();
        if (categoryId.HasValue)
        {
            query = query.Where(p => p.CategoryId == categoryId.Value);
        }

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(p => p.Name.Contains(search));
        }
        
        query = sortByPrice?.ToLower() == "desc" 
            ? query.OrderByDescending(p => p.Price) 
            : query.OrderBy(p => p.Price);

        var products = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize).Select(p => p.ToResponseDto())
            .ToListAsync();

        return Ok(products);

    }

    [HttpGet("api/products/{id:guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var product = await _dbContext.Products
            .AsNoTracking()
            .Include(p => p.Category)
            .SingleOrDefaultAsync(p => p.Id == id);

        return product is null
            ? NotFound()
            : Ok(product.ToResponseDto());
    }

    [HttpPost("api/products")]
    public async Task<IActionResult> Create([FromBody] CreateProductDto dto)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var category = await _dbContext.Categories
            .SingleOrDefaultAsync(c => c.Id == dto.CategoryId);
        if (category is null)
        {
            return NotFound("Category not found.");
        }

        var product = new Product
        {
            Name = dto.Name,
            Price = dto.Price,
            Stock = dto.Stock,
            CategoryId = dto.CategoryId
        };
        await _dbContext.Products.AddAsync(product);
        await _dbContext.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = product.Id }, product.ToResponseDto());
    }
    
    
    
    
    [HttpPut("api/products/{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id ,[FromBody] CreateProductDto dto)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var product = await _dbContext.Products
            .SingleOrDefaultAsync(p => p.Id == id);
        
        if (product is null)
        {
            return NotFound("Product not found.");
        }
        
        var category = await _dbContext.Categories
            .SingleOrDefaultAsync(c => c.Id == dto.CategoryId);
        
        if (category is null)
        {
            return NotFound("Category not found.");
        }

        product.Name = dto.Name;
        product.Price = dto.Price;
        product.Stock = dto.Stock;
        product.CategoryId = dto.CategoryId;
        
        await _dbContext.SaveChangesAsync();
        return NoContent();
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var product = await _dbContext.Products.FindAsync(id);
        if (product == null) return NotFound();

        _dbContext.Products.Remove(product);
        await _dbContext.SaveChangesAsync();
        return NoContent();
    }
}