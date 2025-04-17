using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP_SHOP.Models
{
    [Table("cart_items")]
    public class CartItem : AuditableEntity
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public int ProductColorId { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [ForeignKey("UserId")]
        public virtual User? User { get; set; }

        [ForeignKey("ProductColorId")]
        public virtual ProductColor? ProductColor { get; set; }
    }
}