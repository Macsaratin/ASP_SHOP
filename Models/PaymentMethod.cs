using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP_SHOP.Models
{
    [Table("payment_methods")]
    public class PaymentMethod : AuditableEntity
    {
        [Key]
        public int Id { get; set; }

        public required string Name { get; set; }

        public bool IsActive { get; set; } = true;
    }
}