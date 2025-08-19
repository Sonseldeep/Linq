using Ecommerce.API.DTOs.Category;
using Ecommerce.API.Models;

namespace Ecommerce.API.Mapping;

public static class CategoryMapping
{
    public static CategoryResponseDto ToResponseDto(this Category category)
    {
        return new CategoryResponseDto
        {
            Id = category.Id,
            Name = category.Name
        };
    }
}