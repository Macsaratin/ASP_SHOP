using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP_SHOP.Models
{
    [Table("product_variants")]
    public class ProductVariant : AuditableEntity
    {
        [Key]
        public int Id { get; set; }


        public required string Storage { get; set; }

        [Range(0, double.MaxValue)]
        public decimal BasePrice { get; set; }

        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product? Product { get; set; }

        public virtual IEnumerable<ProductColor> Colors { get; set; } = new List<ProductColor>();
    }
}