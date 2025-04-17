using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP_SHOP.Models
{
    [Table("brands")]
    public class Brand : AuditableEntity
    {
        [Key]
        public int Id { get; set; }

        public required string Name { get; set; }

        public string? ImageUrl { get; set; }

        public virtual List<Product>? Products { get; set; }
    }
}