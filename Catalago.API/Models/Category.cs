using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalago.API.Models;

[Table("categories")]
public class Category
{
    public Category()
    {
        Products = new Collection<Product>();        
    }

    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    [Column("name")]
    public string? Name { get; set; }

    [Required]
    [StringLength(300)]
    [Column("image_url")]
    public string? ImageUrl { get; set; }

    public ICollection<Product>? Products { get; set; }
}
