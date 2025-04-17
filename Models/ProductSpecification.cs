using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP_SHOP.Models
{
    [Table("product_specifications")]
    public class ProductSpecification : AuditableEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        public required string Key { get; set; }

        public required string Value { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product? Product { get; set; }
    }
}