namespace Ecommerce.API.DTOs.Category;

public class CategoryResponseDto
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Name { get; set; } = string.Empty;
}