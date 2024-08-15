using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalago.API.Models;

[Table("products")]
public class Product
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(80)]
    [Column("name")]
    public string? Name { get; set; }

    [Required]
    [StringLength(300)]
    [Column ("description")]
    public string? Description { get; set; }

    [Required]
    [Column("price", TypeName ="decimal(10,2)")]
    public decimal Price { get; set; }

    [Required]
    [StringLength(80)]
    [Column("image_url")]
    public string? ImageUrl { get; set; }

    [Column("quantity")]
    public float Quantity { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("category_id")]
    public int CategoryId { get; set; }

    public Category? Category { get; set; }
}
