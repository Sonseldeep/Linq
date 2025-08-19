namespace Ecommerce.API.DTOs.Product;

public class ProductResponseDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public int Stock { get; set; }
    
    public Guid CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
}