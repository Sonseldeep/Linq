using System.ComponentModel.DataAnnotations;

namespace Ecommerce.API.Models;

public class Category
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [MaxLength(100)]
    public required string Name { get; set; } = string.Empty;
    
    // Navigation property: One Category can have many Products
    public ICollection<Product>? Products { get; set; }
    
}