using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Models;

public class Product
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [MaxLength(150)]
    public required string Name { get; set; } = string.Empty;

    [Range(0,double.MaxValue)]
    [Precision(18,2)]
    public decimal Price { get; set; }

    [Range(0,int.MaxValue)]
    public int Stock { get; set; }
    
    // Navigation: Product belongs to  Category
    public Category? Category { get; set; }
    
    // FK: Product belongs to one Category
    [ForeignKey("Category")]
    public Guid CategoryId { get; set; }
}