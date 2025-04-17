using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP_SHOP.Models
{
    [Table("images")]
    public class Image : AuditableEntity
    {
        [Key]
        public int Id { get; set; }

        public required string ImageUrl { get; set; }

        public int ProductId { get; set; }

        public virtual Product? Product { get; set; }
    }
}