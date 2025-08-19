using Ecommerce.API.DTOs.Product;
using Ecommerce.API.Models;

namespace Ecommerce.API.Mapping;

public static class ProductMapping
{
    public static ProductResponseDto ToResponseDto(this Product product)
    {
        return new ProductResponseDto
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Stock = product.Stock,
            CategoryId = product.CategoryId,
            CategoryName = product.Category?.Name ?? string.Empty
        };
    }
}