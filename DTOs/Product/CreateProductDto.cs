namespace Ecommerce.API.DTOs.Product;

public class CreateProductDto
{
    public required string Name { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public int Stock { get; set; }
    
    public Guid CategoryId { get; set; }
}